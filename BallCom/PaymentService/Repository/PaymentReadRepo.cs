using Microsoft.EntityFrameworkCore;
using PaymentService.Database;
using Shared.Models;
using Shared.Repository.Interface;

namespace PaymentService.Repository;

public class PaymentReadRepo(PaymentDbContext context) : IReadRepository<Payment>
{
    public async Task<Payment> GetByIdAsync(Guid id)
    {
        return await context.Payments.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await context.Payments.ToListAsync();
    }
}