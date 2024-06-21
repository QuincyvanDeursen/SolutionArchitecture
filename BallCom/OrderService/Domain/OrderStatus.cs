﻿namespace OrderService.Domain
{
    public enum OrderStatus
    {
        Placed,
        Paid,
        Cancelled,
        Processing,
        Shipped,
        Delivered
    }
}