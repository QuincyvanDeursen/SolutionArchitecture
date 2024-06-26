using Microsoft.EntityFrameworkCore;
using PaymentService.Database;
using Shared.Models;
using Shared.Repository.Interface;

namespace PaymentService.Repository;

public class CustomerWriteRepo(PaymentDbContext context) : IWriteRepository<PaymentCustomer>
{
    private readonly PaymentDbContext _context = context;
    
    public async Task CreateAsync(PaymentCustomer entity)
    {
        await _context.Customers.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(PaymentCustomer entity)
    {
        var oldCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == entity.Id);
        if(oldCustomer == null) throw new KeyNotFoundException("The customer was not found in the payment database");
        
        // Customer can only update name and address (cant update id)
        oldCustomer.Name = entity.Name;
        oldCustomer.Address = entity.Address;
        
        _context.Customers.Update(oldCustomer);
        await _context.SaveChangesAsync();
    }
}