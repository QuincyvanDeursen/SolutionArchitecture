using InventoryManagement.CQRS.Queries.Interfaces;
using InventoryManagement.Domain;

namespace InventoryManagement.CQRS.Queries
{
    public class GetAllProductsQuery : IQuery<IEnumerable<Product>>
    {

    }
}
