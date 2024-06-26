using Shared.Models;

namespace OrderService.DTO;

public class OrderUpdateStatusDto
{
    public OrderStatus Status { get; set; }
}