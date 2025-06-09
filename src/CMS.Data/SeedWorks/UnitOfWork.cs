using AutoMapper;
using CMS.Core.Domain.Identity;
using CMS.Core.Repositories;
using CMS.Core.SeedWorks;
using CMS.Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CMS.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CMSDbContext _context;
        public UnitOfWork(CMSDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            Posts = new PostRepository(context, mapper, userManager);
            PostCategories = new PostCategoryRepository(context, mapper);
            Series = new SeriesRepository(context, mapper);
        }

        public IPostRepository Posts { get; private set; }

        public IPostCategoryRepository PostCategories { get; private set; }

        public ISeriesRepository Series { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
