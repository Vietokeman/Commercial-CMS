using CMS.Core.Domain.Royalty;
using CMS.Core.Models;
using CMS.Core.Models.Royalty;
using CMS.Core.Repositories;
using System.Linq.Expressions;

namespace CMS.Data.Repositories
{
    public class MockTransactionRepository : ITransactionRepository
    {
        private readonly List<Transaction> _mockTransactions;

        public MockTransactionRepository()
        {
            _mockTransactions = new List<Transaction>
            {
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    FromUserName = "admin",
                    FromUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    ToUserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    ToUserName = "janesmith",
                    Amount = 350000,
                    TransactionType = TransactionType.RoyaltyPay,
                    DateCreated = DateTime.Now.AddDays(-25),
                    Note = "Thanh toán nhuận bút tháng 11/2025"
                },
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    FromUserName = "admin",
                    FromUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    ToUserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    ToUserName = "janesmith",
                    Amount = 420000,
                    TransactionType = TransactionType.RoyaltyPay,
                    DateCreated = DateTime.Now.AddDays(-55),
                    Note = "Thanh toán nhuận bút tháng 10/2025"
                },
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    FromUserName = "admin",
                    FromUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    ToUserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    ToUserName = "mikej",
                    Amount = 280000,
                    TransactionType = TransactionType.RoyaltyPay,
                    DateCreated = DateTime.Now.AddDays(-30),
                    Note = "Thanh toán nhuận bút tháng 11/2025"
                },
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    FromUserName = "admin",
                    FromUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    ToUserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    ToUserName = "mikej",
                    Amount = 195000,
                    TransactionType = TransactionType.RoyaltyPay,
                    DateCreated = DateTime.Now.AddDays(-60),
                    Note = "Thanh toán nhuận bút tháng 10/2025"
                },
                new Transaction
                {
                    Id = Guid.NewGuid(),
                    FromUserName = "admin",
                    FromUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    ToUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    ToUserName = "admin",
                    Amount = 500000,
                    TransactionType = TransactionType.RoyaltyPay,
                    DateCreated = DateTime.Now.AddDays(-20),
                    Note = "Thanh toán nhuận bút tháng 11/2025"
                }
            };
        }



        public void Remove(Transaction transaction)
        {
            _mockTransactions.Remove(transaction);
        }

        public Task<PageResult<TransactionDto>> GetAllPaging(string? keyword, int fromMonth, int fromYear, 
            int toMonth, int toYear, int pageIndex = 1, int pageSize = 10)
        {
            var query = _mockTransactions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.FromUserName.Contains(keyword) || x.ToUserName.Contains(keyword));
            }

            // Filter by date range
            query = query.Where(x =>
                x.DateCreated.Year >= fromYear && x.DateCreated.Month >= fromMonth &&
                x.DateCreated.Year <= toYear && x.DateCreated.Month <= toMonth
            );

            var totalRow = query.Count();
            var transactions = query.OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TransactionDto
                {
                    Id = x.Id,
                    FromUserName = x.FromUserName,
                    FromUserId = x.FromUserId,
                    ToUserId = x.ToUserId,
                    ToUserName = x.ToUserName,
                    Amount = x.Amount,
                    TransactionType = x.TransactionType,
                    DateCreated = x.DateCreated,
                    Note = x.Note
                })
                .ToList();

            return Task.FromResult(new PageResult<TransactionDto>
            {
                Results = transactions,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            });
        }

        public Task<Transaction?> GetByIdAsync<TKey>(TKey id)
        {
            var guid = (Guid)(object)id!;
            var transaction = _mockTransactions.FirstOrDefault(x => x.Id == guid);
            return Task.FromResult(transaction);
        }

        public Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Transaction>>(_mockTransactions);
        }

        public IEnumerable<Transaction> Find(Expression<Func<Transaction, bool>> expression)
        {
            return _mockTransactions.AsQueryable().Where(expression);
        }

        public Task Add(Transaction entity)
        {
            entity.Id = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;
            _mockTransactions.Add(entity);
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<Transaction> entities)
        {
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.DateCreated = DateTime.Now;
                _mockTransactions.Add(entity);
            }
            return Task.CompletedTask;
        }

        public void RemoveRange(IEnumerable<Transaction> entities)
        {
            foreach (var entity in entities)
            {
                _mockTransactions.Remove(entity);
            }
        }
    }
}
