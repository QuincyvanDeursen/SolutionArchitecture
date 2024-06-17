namespace OrderService.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int? PaymentId { get; set; } // Nullable, mogelijk niet nodig als payment ook direct aangemaakt wordt.
        public ICollection<OrderItem>? OrderItems { get; set; } //Nullable, maar mag inprincipe niet leeg zijn.

    }
}
