using CMS.Core.Domain.Identity;
using CMS.Core.Domain.Royalty;
using CMS.Core.SeedWorks;
using CMS.Core.Services;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CMS.Data.Service
{
    public class RoyaltyService : IRoyaltyService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        
        public RoyaltyService(UserManager<AppUser> userManager,
            IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        
        public async Task<List<RoyaltyReportByMonthDto>> GetRoyaltyReportByMonthAsync(Guid? userId, int fromMonth, int fromYear, int toMonth, int toYear)
        {
            using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed) await conn.OpenAsync();
                
                var coreSql = @"
                    SELECT 
                        CAST(EXTRACT(month FROM p.""DateCreated"") AS INTEGER) as Month,
                        CAST(EXTRACT(year FROM p.""DateCreated"") AS INTEGER) as Year,
                        SUM(CASE WHEN p.""Status"" = 0 THEN 1 ELSE 0 END) as NumberOfDraftPosts,
                        SUM(CASE WHEN p.""Status"" = 1 THEN 1 ELSE 0 END) as NumberOfWaitingApprovalPosts,
                        SUM(CASE WHEN p.""Status"" = 2 THEN 1 ELSE 0 END) as NumberOfRejectedPosts,
                        SUM(CASE WHEN p.""Status"" = 3 THEN 1 ELSE 0 END) as NumberOfPublishPosts,
                        SUM(CASE WHEN p.""Status"" = 3 AND p.""IsPaid"" = true THEN 1 ELSE 0 END) as NumberOfPaidPublishPosts,
                        SUM(CASE WHEN p.""Status"" = 3 AND p.""IsPaid"" = false THEN 1 ELSE 0 END) as NumberOfUnpaidPublishPosts
                    FROM ""Posts"" p
                    GROUP BY 
                        EXTRACT(month FROM p.""DateCreated""),
                        EXTRACT(year FROM p.""DateCreated""),
                        p.""AuthorUserId""
                    HAVING 
                        (@fromMonth = 0 OR EXTRACT(month FROM p.""DateCreated"") >= @fromMonth) 
                        AND (@fromYear = 0 OR EXTRACT(year FROM p.""DateCreated"") >= @fromYear)
                        AND (@toMonth = 0 OR EXTRACT(month FROM p.""DateCreated"") <= @toMonth)
                        AND (@toYear = 0 OR EXTRACT(year FROM p.""DateCreated"") <= @toYear)
                        AND (@userId IS NULL OR p.""AuthorUserId"" = @userId)";

                var items = await conn.QueryAsync<RoyaltyReportByMonthDto>(coreSql, new
                {
                    fromMonth,
                    fromYear,
                    toMonth,
                    toYear,
                    userId
                }, null, 120, CommandType.Text);
                
                return items.ToList();
            }
        }

        public async Task<List<RoyaltyReportByUserDto>> GetRoyaltyReportByUserAsync(Guid? userId, int fromMonth, int fromYear, int toMonth, int toYear)
        {
            using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed) await conn.OpenAsync();
                
                var coreSql = @"
                    SELECT 
                        u.""Id"" as UserId,
                        u.""UserName"" as UserName,
                        SUM(CASE WHEN p.""Status"" = 0 THEN 1 ELSE 0 END) as NumberOfDraftPosts,
                        SUM(CASE WHEN p.""Status"" = 1 THEN 1 ELSE 0 END) as NumberOfWaitingApprovalPosts,
                        SUM(CASE WHEN p.""Status"" = 2 THEN 1 ELSE 0 END) as NumberOfRejectedPosts,
                        SUM(CASE WHEN p.""Status"" = 3 THEN 1 ELSE 0 END) as NumberOfPublishPosts,
                        SUM(CASE WHEN p.""Status"" = 3 AND p.""IsPaid"" = true THEN 1 ELSE 0 END) as NumberOfPaidPublishPosts,
                        SUM(CASE WHEN p.""Status"" = 3 AND p.""IsPaid"" = false THEN 1 ELSE 0 END) as NumberOfUnpaidPublishPosts
                    FROM ""Posts"" p 
                    INNER JOIN ""AspNetUsers"" u ON p.""AuthorUserId"" = u.""Id""
                    GROUP BY 
                        EXTRACT(month FROM p.""DateCreated""),
                        EXTRACT(year FROM p.""DateCreated""),
                        p.""AuthorUserId"",
                        u.""Id"",
                        u.""UserName""
                    HAVING 
                        (@fromMonth = 0 OR EXTRACT(month FROM p.""DateCreated"") >= @fromMonth) 
                        AND (@fromYear = 0 OR EXTRACT(year FROM p.""DateCreated"") >= @fromYear)
                        AND (@toMonth = 0 OR EXTRACT(month FROM p.""DateCreated"") <= @toMonth)
                        AND (@toYear = 0 OR EXTRACT(year FROM p.""DateCreated"") <= @toYear)
                        AND (@userId IS NULL OR p.""AuthorUserId"" = @userId)";

                var items = await conn.QueryAsync<RoyaltyReportByUserDto>(coreSql, new
                {
                    fromMonth,
                    fromYear,
                    toMonth,
                    toYear,
                    userId
                }, null, 120, CommandType.Text);
                
                return items.ToList();
            }
        }

        public async Task PayRoyaltyForUserAsync(Guid fromUserId, Guid toUserId)
        {
            var fromUser = await _userManager.FindByIdAsync(fromUserId.ToString());
            if (fromUser == null)
            {
                throw new Exception($"User {fromUserId} not found");
            }
            
            var toUser = await _userManager.FindByIdAsync(toUserId.ToString());
            if (toUser == null)
            {
                throw new Exception($"User {toUserId} not found");
            }
            
            var unpaidPublishPosts = await _unitOfWork.Posts.GetListUnpaidPublishPosts(toUserId);
            double totalRoyalty = 0;
            
            foreach (var post in unpaidPublishPosts)
            {
                post.IsPaid = true;
                post.PaidDate = DateTime.UtcNow;
                //post.RoyaltyAmount = toUser.RoyaltyAmountPerPost;
                //totalRoyalty += toUser.RoyaltyAmountPerPost;
            }

            toUser.Balance += totalRoyalty;
            await _userManager.UpdateAsync(toUser);
            
            _unitOfWork.Transactions.Add(new Transaction()
            {
                FromUserId = fromUser.Id,
                FromUserName = fromUser.UserName,
                ToUserId = toUserId,
                ToUserName = toUser.UserName,
                Amount = totalRoyalty,
                TransactionType = TransactionType.RoyaltyPay,
                Note = $"{fromUser.UserName} thanh toán nhuận bút cho {toUser.UserName}"
            });
            
            await _unitOfWork.CompleteAsync();
        }
    }
}