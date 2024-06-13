using InventoryService.Database;
using InventoryService.Events;
using Shared.Repository.Interface;

namespace InventoryService.Repository
{
    public class InventoryEventRepo : IWriteRepository<InventoryBaseEvent>
    {
        private readonly InventoryDbContext _context;

        public InventoryEventRepo(InventoryDbContext context)
        {
            _context = context;
        }

        public void Save(InventoryBaseEvent @event)
        {
            _context.InventoryEvents.Add(@event);
        }

        public void Delete(InventoryBaseEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
