using ProductMicroServices.Models;
using ProductMicroServices.Repositories;

namespace ProductMicroServices.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync() => _repo.GetAllProductsAsync();

        public Task<Product?> GetProductByIdAsync(int id) => _repo.GetProductByIdAsync(id);

        public Task<Product> AddProductAsync(Product product) => _repo.AddProductAsync(product);

        public Task<Product?> UpdateProductAsync(Product product) => _repo.UpdateProductAsync(product);

        public Task<bool> DeleteProductAsync(int id) => _repo.DeleteProductAsync(id);
    }
}
