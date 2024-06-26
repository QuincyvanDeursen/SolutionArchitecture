using OrderService.Domain;
using Shared.Models;

namespace OrderService.Services.Mappers;

public static class OrderRelatedEntityMapper
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