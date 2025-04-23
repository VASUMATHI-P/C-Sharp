using Xunit;
using Moq;
using ProductMicroServices.Models;
using ProductMicroServices.Repositories;
using ProductMicroServices.Services;
using ProductMicroServices.DTOs;

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
        var productDto = new ProductCreateDto { Name = "Camel Pen", Category = "Stationery", Price = 10, Stock = 150 };
        var newProduct = new Product { Id = 1, Name = "Camel Pen", Category = "Stationery", Price = 10, Stock = 150 };

        _mockRepo.Setup(r => r.AddProductAsync(It.IsAny<Product>())).ReturnsAsync(newProduct);

        var result = await _service.AddProductAsync(productDto);

        Assert.NotNull(result);
        Assert.Equal(newProduct.Name, result.Name);

        _mockRepo.Verify(r => r.AddProductAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task UpdateProduct_UpdatesProductSuccessfully()
    {
        var product = new Product { Id = 1, Name = "Nataraj Pencil", Category = "Stationery", Price = 5, Stock = 200 };
        var updatedProduct = new Product { Id = 1, Name = "Updated Nataraj Pencil", Category = "Stationery", Price = 6, Stock = 250 };

        _mockRepo.Setup(r => r.GetProductByIdAsync(product.Id)).ReturnsAsync(product);
        _mockRepo.Setup(r => r.UpdateProductAsync(updatedProduct)).ReturnsAsync(updatedProduct);

        var result = await _service.UpdateProductAsync(updatedProduct);

        Assert.NotNull(result);
        Assert.Equal(updatedProduct.Name, result.Name);
        Assert.Equal(updatedProduct.Price, result.Price);

        _mockRepo.Verify(r => r.GetProductByIdAsync(product.Id), Times.Once);
        _mockRepo.Verify(r => r.UpdateProductAsync(updatedProduct), Times.Once);
    }


    [Fact]
    public async Task DeleteProduct_DeletesProductSuccessfully()
    {
        var existingProduct = new Product { Id = 1, Name = "Nataraj Pencil", Category = "Stationery", Price = 5, Stock = 200 };

        _mockRepo.Setup(r => r.GetProductByIdAsync(existingProduct.Id)).ReturnsAsync(existingProduct);
        _mockRepo.Setup(r => r.DeleteProductAsync(existingProduct.Id)).ReturnsAsync(true);

        var result = await _service.DeleteProductAsync(existingProduct.Id);

        Assert.True(result);
    }

}
