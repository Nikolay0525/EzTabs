using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Generic.Repository
{
    public interface IRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task Add(T entity);
        Task Add(IEnumerable<T> entity);
        Task Update(T entity);
        Task Update(IEnumerable<T> entity);
        Task Delete(T entity);
        Task Delete(IEnumerable<T> entity);
    }
}
