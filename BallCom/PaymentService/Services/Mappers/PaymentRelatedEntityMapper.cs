using Shared.Models;

namespace PaymentService.Services.Mapper;

public static class PaymentRelatedEntityMapper 
{
    public static PaymentOrder MapOrderToPaymentOrder(Order order)
    {
        return new PaymentOrder
        {
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            Address = order.Address,
            Status = order.Status,
            CustomerId = order.CustomerId
        };
    } 
    
    public static PaymentCustomer MapCustomerToPaymentCustomer(Customer customer)
    {
        return new PaymentCustomer
        {
            Id = customer.Id,
            Name = $"{customer.FirstName} {customer.LastName}",
            Address = customer.Address
        };
    }
}