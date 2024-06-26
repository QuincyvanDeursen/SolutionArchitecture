using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repository.Interface
{
    public interface IReadRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllByIdAsync(Guid aggergateId);
    }
}
