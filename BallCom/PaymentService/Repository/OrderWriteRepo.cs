using Microsoft.EntityFrameworkCore;
using PaymentService.Database;
using Shared.Models;
using Shared.Models.Payment;
using Shared.Repository.Interface;

namespace PaymentService.Repository;

public class OrderWriteRepo(PaymentDbContext context) : IWriteRepository<PaymentOrder>
{
    private readonly PaymentDbContext _context = context;

    public async Task CreateAsync(PaymentOrder entity)
    {
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PaymentOrder entity)
    {
        var oldOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == entity.Id);

        if (oldOrder == null)
        {
            throw new KeyNotFoundException("Order was not found in the payment database");
        }

        // Update order fields
        oldOrder.Address = entity.Address;
        oldOrder.Status = entity.Status;
        
        _context.Orders.Update(oldOrder);
        await _context.SaveChangesAsync();
    }
}