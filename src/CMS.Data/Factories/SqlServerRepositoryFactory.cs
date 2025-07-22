using CMS.Core.Domain.Content;
using CMS.Core.SeedWorks;
using System.Linq.Expressions;

namespace CMS.Core.Factories
{
    public class SqlServerRepositoryFactory
    {
        // SQL Server   
        public class SqlPostRepository : IRepository<Post, Guid>
        {
            public Task Add(Post entity)
            {
                throw new NotImplementedException();
            }

            public Task AddRange(IEnumerable<Post> entities)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Post> Find(Expression<Func<Post, bool>> expression)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Post>> GetAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Post?> GetByIdAsync<TKey>(TKey id)
            {
                throw new NotImplementedException();
            }

            public void Remove(Post entity)
            {
                throw new NotImplementedException();
            }

            public void RemoveRange(IEnumerable<Post> entities)
            {
                throw new NotImplementedException();
            }
        }

        // PostgreSQL
        public class PgPostRepository : IRepository<Post, Guid>
        {
            public Task Add(Post entity)
            {
                throw new NotImplementedException();
            }

            public Task AddRange(IEnumerable<Post> entities)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Post> Find(Expression<Func<Post, bool>> expression)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Post>> GetAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Post?> GetByIdAsync<TKey>(TKey id)
            {
                throw new NotImplementedException();
            }

            public void Remove(Post entity)
            {
                throw new NotImplementedException();
            }

            public void RemoveRange(IEnumerable<Post> entities)
            {
                throw new NotImplementedException();
            }
        }

    }
}
