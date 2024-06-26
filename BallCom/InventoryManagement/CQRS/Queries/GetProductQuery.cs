using InventoryManagement.CQRS.Queries.Interfaces;
using InventoryManagement.Domain;

namespace InventoryManagement.CQRS.Queries
{
    public class GetProductQuery : IQuery<Product>
    {
        public Guid Id { get; set; }

        public GetProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
