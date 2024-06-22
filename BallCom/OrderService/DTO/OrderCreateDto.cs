namespace OrderService.DTO
{
    public class OrderCreateDto
    {
        public Guid CustomerId { get; set; }
        public string? Address { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
        
    }
}
