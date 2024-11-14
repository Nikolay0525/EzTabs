using System.Linq.Expressions;

namespace EzTabs.Data.Repository
{
    public interface IRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Add(IEnumerable<T> entity);
        Task Update(T entity);
        Task Update(IEnumerable<T> entity);
        Task Delete(T entity);
        Task Delete(IEnumerable<T> entity);

    }
}
