using Xunit;
using Moq;
using ProductMicroServices.Models;
using ProductMicroServices.Repositories;
using ProductMicroServices.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProductServiceTests
{
    private readonly ProductService _service;
    private readonly Mock<IProductRepository> _mockRepo;

    public ProductServiceTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsProductList()
    {
        var products = new List<Product>
        {
            new() { Id = 1, Name = "Nataraj Pencil", Category = "Stationery", Price = 5, Stock = 200 },
            new() { Id = 2, Name = "Apsara Pencil", Category = "Stationery", Price = 5, Stock = 200 }
        };

        _mockRepo.Setup(r => r.GetAllProductsAsync()).ReturnsAsync(products);

        var result = await _service.GetAllProductsAsync();

        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Count());
    }

    [Fact]
    public async Task GetProductById_ReturnsProduct()
    {
        var product = new Product { Id = 1, Name = "Nataraj Pencil", Category = "Stationery", Price = 5, Stock = 200 };

        _mockRepo.Setup(r => r.GetProductByIdAsync(product.Id)).ReturnsAsync(product);

        var result = await _service.GetProductByIdAsync(product.Id);

        Assert.NotNull(result);
        Assert.Equal(product.Name, result.Name);
    }

    [Fact]
    public async Task GetProductById_ReturnsNullWhenNotFound()
    {
        var productId = 100;

        _mockRepo.Setup(r => r.GetProductByIdAsync(productId)).ReturnsAsync((Product)null);

        var result = await _service.GetProductByIdAsync(productId);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddProduct_AddsProductSuccessfully()
    {
        var newProduct = new Product { Id = 3, Name = "Camel Pen", Category = "Stationery", Price = 10, Stock = 150 };

        _mockRepo.Setup(r => r.AddProductAsync(newProduct)).ReturnsAsync(newProduct);

        var result = await _service.AddProductAsync(newProduct);

        Assert.NotNull(result);
        Assert.Equal(newProduct.Name, result.Name);
        _mockRepo.Verify(r => r.AddProductAsync(newProduct), Times.Once);
    }

    [Fact]
    public async Task UpdateProduct_UpdatesProductSuccessfully()
    {
        var product = new Product { Id = 1, Name = "Nataraj Pencil", Category = "Stationery", Price = 5, Stock = 200 };

        _mockRepo.Setup(r => r.UpdateProductAsync(product)).ReturnsAsync(product);

        var updatedProduct = await _service.UpdateProductAsync(product);

        Assert.NotNull(updatedProduct);
        Assert.Equal(product.Name, updatedProduct.Name);
        _mockRepo.Verify(r => r.UpdateProductAsync(product), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_DeletesProductSuccessfully()
    {
        _mockRepo.Setup(r => r.DeleteProductAsync(1)).ReturnsAsync(true);

        var result = await _service.DeleteProductAsync(1);

        Assert.True(result);
        _mockRepo.Verify(r => r.DeleteProductAsync(1), Times.Once);
    }
}
