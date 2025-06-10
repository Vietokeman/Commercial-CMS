using CMS.Core.Domain.Royalty;
using CMS.Core.Models;
using CMS.Core.Models.Royalty;
using CMS.Core.SeedWorks;

namespace CMS.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        Task<PageResult<TransactionDto>> GetAllPaging(string? userName, int fromMonth, int formYear, int toMonth, int toYear, int pageIndex = 1, int pageSize = 10);


    }
}
