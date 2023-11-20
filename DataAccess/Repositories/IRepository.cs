using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepository<T>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> Find(Guid id);
        Task<T> Get(Guid id);
        
        Task<T> Update(T item);
        Task<T> Delete(Guid id);
    }
}
