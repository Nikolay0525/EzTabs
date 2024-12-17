using System.Linq.Expressions;

namespace EzTabs.Data.Repository
{
    public interface IRepo<T> where T : class
    {
        Task<OperationResult<IEnumerable<T>>> GetAll();
        Task<OperationResult<T>> GetById(Guid id);
        Task<OperationResult<bool>> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<OperationResult<T>> Add(T entity);
        Task<OperationResult<T>> Add(IEnumerable<T> entity);
        Task<OperationResult<T>> Update(T entity);
        Task<OperationResult<T>> Update(IEnumerable<T> entity);
        Task<OperationResult<T>> Delete(T entity);
        Task<OperationResult<T>> Delete(IEnumerable<T> entity);

    }
}
