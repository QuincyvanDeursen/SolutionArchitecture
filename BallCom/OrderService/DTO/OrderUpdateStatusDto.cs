using Shared.Models;
using Shared.Models.Order;

namespace OrderService.DTO;

public class OrderUpdateStatusDto
{
    public OrderStatus Status { get; set; }
}