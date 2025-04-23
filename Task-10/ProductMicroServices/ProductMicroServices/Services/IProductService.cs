using ProductMicroServices.DTOs;
using ProductMicroServices.Models;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(ProductCreateDto productDto);
    Task<Product?> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
}
