using Shared.Models;
using Shared.Models.Order;
using Shared.Models.Payment;

namespace OrderService.Extensions;

public static class OrderEntityMapperExtensions
{
    public static OrderPayment ToOrderPayment(this Payment payment)
    {
        return new OrderPayment
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            CustomerId = payment.CustomerId,
            Status = payment.Status
        };
    }
}