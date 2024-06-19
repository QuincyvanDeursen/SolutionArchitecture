using Shared.EventSourcing;

namespace Shared.Repository.Interface
{
    public interface IWriteRepository<in T> where T : Event
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
