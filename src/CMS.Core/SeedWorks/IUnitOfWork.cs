using CMS.Core.Repositories;
using CMS.Core.Services;

namespace CMS.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        IPostCategoryRepository PostCategories { get; }
        ISeriesRepository Series { get; }
        ITransactionRepository Transactions { get; }
        IRoyaltyService RoyaltyService { get; }
        Task<int> CompleteAsync();
    }
}
