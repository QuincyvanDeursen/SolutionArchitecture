namespace OrderService.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int? PaymentId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
