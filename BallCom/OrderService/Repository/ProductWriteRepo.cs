using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using Shared.Models.Inventory;
using Shared.Models.Order;
using Shared.Repository.Interface;

namespace OrderService.Repository;

public class ProductWriteRepo(OrderDbContext context) : IWriteRepository<OrderProduct>
{
    public async Task CreateAsync(OrderProduct entity)
    {
        context.Products.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderProduct entity)
    {
        var oldProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == entity.Id);
        if (oldProduct == null)
        {
            throw new KeyNotFoundException("Product was not found in the order database");
        }
        
        // Update only changeable fields
        oldProduct.Name = entity.Name;
        oldProduct.Price = entity.Price;
        
        context.Products.Update(oldProduct);
        await context.SaveChangesAsync();
    }
}