namespace OrderService.DTO
{
    public class OrderCreateDto
    {
        public Guid CustomerId { get; set; }
        public string? Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
