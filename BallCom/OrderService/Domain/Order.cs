namespace OrderService.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int? PaymentId { get; set; } 
        public string? Adress {  get; set; }
        public string? Postalcode { get; set; }
        public string? City { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
