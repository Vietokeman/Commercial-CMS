using CMS.Core.Domain.Content;
using CMS.Core.Models;
using CMS.Core.Models.Content;
using CMS.Core.SeedWorks;

namespace CMS.Core.Repositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory, Guid>
    {
        Task<PostCategoryDto> GetBySlug(string slug);

        Task<PageResult<PostCategoryDto>> GetPostCategorysPagingAsync(string? keyword, int PageIndex = 1, int pageSize = 10);
    }
}
