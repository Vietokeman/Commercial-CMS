using CMS.Core.Domain.Content;
using CMS.Core.Repositories;
using CMS.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post, Guid>, IPostRepository
    {
        public PostRepository(CMSDbContext context) : base(context)
        {

        }

        public async Task<List<Post>> GetPopularPosts(int count)
        {
            return await _context.Posts.OrderByDescending(x => x.ViewCount).Take(count).ToListAsync();
        }
    }
}
