using Microsoft.EntityFrameworkCore;
using PaymentService.Database;
using Shared.Models;
using Shared.Models.Payment;
using Shared.Repository.Interface;

namespace PaymentService.Repository;

public class PaymentWriteRepo(PaymentDbContext context) : IWriteRepository<Payment>
{
    public async Task CreateAsync(Payment entity)
    {
        await context.Payments.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Payment entity)
    {
        context.Payments.Update(entity);
        await context.SaveChangesAsync();
    }
}