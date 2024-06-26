using Shared.Models;
using Shared.Models.Customer;
using Shared.Models.Order;
using Shared.Models.Payment;

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
    
    public static OrderCustomer ToOrderCustomer(this Customer customer)
    {
        return new OrderCustomer
        {
            Id = customer.Id,
            Name = $"{customer.FirstName} {customer.LastName}",
            Address = customer.Address,
            PhoneNumber = customer.PhoneNumber
        };
    }
}