﻿using OrderService.Domain;
using OrderService.DTO;

namespace OrderService.Repository.Interface
{
    public interface IOrderRepo
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Order GetOrder(int id);
        Task<Order> SaveOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
