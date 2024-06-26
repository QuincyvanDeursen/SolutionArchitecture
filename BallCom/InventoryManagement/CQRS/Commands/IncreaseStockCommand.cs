using InventoryManagement.CQRS.Commands.Interfaces;

namespace InventoryManagement.CQRS.Commands
{
    public class IncreaseStockCommand : ICommand
    {
        public Guid AggregateId { get; set; }
        public int Amount { get; set; }

        public IncreaseStockCommand(Guid aggregateId, int amount)
        {
            AggregateId = aggregateId;
            Amount = amount;
        }
    }
}
