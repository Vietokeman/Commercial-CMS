using CMS.Core.Domain.Content;
using CMS.Core.SeedWorks;

namespace CMS.Core.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        Task<List<Post>> GetPopularPosts(int count);

    }
}
