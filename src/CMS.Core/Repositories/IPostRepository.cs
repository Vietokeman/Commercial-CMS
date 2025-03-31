using CMS.Core.Domain.Content;
using CMS.Core.Models;
using CMS.Core.Models.Content;
using CMS.Core.SeedWorks;

namespace CMS.Core.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        Task<List<Post>> GetPopularPosts(int count);
        Task<PageResult<PostInListDto>> GetPostsPagingAsync(string? keyword, Guid? categoryId, int PageIndex = 1, int pageSize = 10);
    }
}
