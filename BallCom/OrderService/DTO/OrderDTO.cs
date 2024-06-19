﻿namespace OrderService.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int? PaymentId { get; set; }
        public string? Adress { get; set; }
        public string? Postalcode { get; set; }
        public string? City { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        
    }
}
