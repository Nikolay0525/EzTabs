using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Data.Repository
{
    public interface IRepo<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
