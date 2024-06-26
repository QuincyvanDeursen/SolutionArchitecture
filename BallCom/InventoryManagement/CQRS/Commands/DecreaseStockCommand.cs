using InventoryManagement.CQRS.Commands.Interfaces;

namespace InventoryManagement.CQRS.Commands
{
    public class DecreaseStockCommand : ICommand
    {
        public Guid AggregateId { get; set; }
        public int Amount { get; set; }

        public DecreaseStockCommand(Guid aggregateId, int amount)
        {
            AggregateId = aggregateId;
            Amount = amount;
        }
    }
}
