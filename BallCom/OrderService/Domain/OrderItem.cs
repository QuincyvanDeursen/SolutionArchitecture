namespace OrderService.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Order? Order { get; set; } // nullable, maar mag dat inprincipe niet zijn.
    }
}
