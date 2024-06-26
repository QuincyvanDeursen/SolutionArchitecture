namespace InventoryManagement.Commands
{
    public class IncreaseStockCommand
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
