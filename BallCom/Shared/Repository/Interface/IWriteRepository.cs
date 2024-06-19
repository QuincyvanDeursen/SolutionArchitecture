using Shared.EventSourcing;

namespace Shared.Repository.Interface
{
    public interface IWriteRepository<in T> where T : Event
    {
        Task Save(T @event);
    }
}
