using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository.Interface
{
    public interface IReadRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(Guid id, T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
