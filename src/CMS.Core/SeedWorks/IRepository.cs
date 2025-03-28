using System.Linq.Expressions;

namespace CMS.Core.SeedWorks
{
    public interface IRepository<T, Key> where T : class
    {
        Task<T?> GetByIdAsync<TKey>(TKey id);
        //IEnumerable tra ve tat ca du lieu kieu T
        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        Task Add(T entity);

        Task AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
