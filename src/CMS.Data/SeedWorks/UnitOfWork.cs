using AutoMapper;
using CMS.Core.Repositories;
using CMS.Core.SeedWorks;
using CMS.Data.Repositories;

namespace CMS.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CMSDbContext _context;
        public UnitOfWork(CMSDbContext context, IMapper mapper)
        {
            _context = context;
            Posts = new PostRepository(context, mapper);
        }

        public IPostRepository Posts { get; private set; }

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
