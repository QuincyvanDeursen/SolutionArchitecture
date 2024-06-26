using OrderService.Domain;
using Shared.Models;

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