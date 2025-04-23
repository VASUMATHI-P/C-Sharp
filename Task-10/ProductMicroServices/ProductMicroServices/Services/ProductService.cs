using ProductMicroServices.Models;
using ProductMicroServices.Repositories;
using ProductMicroServices.DTOs;
using ProductMicroServices.Mappers;

namespace ProductMicroServices.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _repo.GetAllProductsAsync();
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            return _repo.GetProductByIdAsync(id);
        }

        public Task<Product> AddProductAsync(ProductCreateDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new ArgumentException("Product name cannot be empty");

            if (productDto.Price <= 0)
                throw new ArgumentException("Product price must be positive");

            var product = ProductMapper.MapToEntity(productDto);
            return _repo.AddProductAsync(product);
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var existingProduct = await _repo.GetProductByIdAsync(product.Id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found :)");
            }
            return await _repo.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existingProduct = await _repo.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found :)");
            }
            return await _repo.DeleteProductAsync(id);
        }
    }
}
