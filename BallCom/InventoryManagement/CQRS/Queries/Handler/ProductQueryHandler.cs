using InventoryManagement.CQRS.Queries.Interfaces;
using InventoryManagement.Domain;
using Shared.Repository.Interface;

namespace InventoryManagement.CQRS.Queries.Handler
{
    public class ProductQueryHandler :
        IQueryHandler<GetProductQuery, Product>,
        IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IReadRepository<Product> _productRepository;

        public ProductQueryHandler(IReadRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request)
        {
            return _productRepository.GetAllAsync();
        }

        public async Task<Product> Handle(GetProductQuery request)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return product;
        }
    }
}
