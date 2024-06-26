namespace InventoryManagement.Domain
{
    public class DummyOrder
    {
        public Guid OrderId { get; set; }
        public List<DummyItem> Items { get; set; }
    }

    public class DummyItem
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}
