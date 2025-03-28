using CMS.Core.SeedWorks;

namespace CMS.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CMSDbContext _context;
        public UnitOfWork(CMSDbContext context)
        {
            _context = context;
        }
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
