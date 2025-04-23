using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductMicroServices.Controllers;
using ProductMicroServices.Models;
using ProductMicroServices.DTOs;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllProducts_ReturnsOkResult()
    {
        var products = new List<Product> {
            new Product { Id = 1, Name = "Pen", Category = "Stationery", Price = 10, Stock = 100 }
        };
        _mockService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);
        var result = await _controller.GetAllProducts();
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.Equal(products.Count, ((List<Product>)returnProducts).Count);
    }

    [Fact]
    public async Task GetProductById_ReturnsProductSuccessfully()
    {
        var product = new Product { Id = 2, Name = "Book", Category = "Stationery", Price = 100, Stock = 50 };
        _mockService.Setup(s => s.GetProductByIdAsync(product.Id)).ReturnsAsync(product);
        var result = await _controller.GetProductById(product.Id);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(product, okResult.Value);
    }

    [Fact]
    public async Task GetProductById_ReturnsNotFound_WhenNotFound()
    {
        _mockService.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync((Product)null);
        var result = await _controller.GetProductById(1);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AddProduct_AddsProductSuccessfully()
    {
        var productDto = new ProductCreateDto { Name = "Book", Category = "Stationery", Price = 100, Stock = 50 };
        var product = new Product {Id = 1, Name = "Book", Category = "Stationery", Price = 100, Stock = 50 };
        _mockService.Setup(s => s.AddProductAsync(productDto)).ReturnsAsync(product);

        var result = await _controller.CreateProduct(productDto);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdAtActionResult.StatusCode);
        Assert.Equal(product, createdAtActionResult.Value);
    }


    [Fact]
    public async Task UpdateProduct_ReturnsOk_IfSuccessful()
    {
        var product = new Product { Id = 1, Name = "Pen Updated", Category = "Stationery", Price = 15, Stock = 80 };
        _mockService.Setup(s => s.UpdateProductAsync(product)).ReturnsAsync(product);
        var result = await _controller.UpdateProduct(1, product);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(product, okResult.Value);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsNotFound_IfNull()
    {
        var product = new Product { Id = 1, Name = "Pen Updated", Category = "Stationery", Price = 15, Stock = 80 };
        _mockService.Setup(s => s.UpdateProductAsync(product)).ReturnsAsync((Product)null);
        var result = await _controller.UpdateProduct(1, product);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent_IfSuccessful()
    {
        _mockService.Setup(s => s.DeleteProductAsync(1)).ReturnsAsync(true);
        var result = await _controller.DeleteProduct(1);
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
        _mockService.Verify(s => s.DeleteProductAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNotFound_IfFails()
    {
        _mockService.Setup(s => s.DeleteProductAsync(1)).ReturnsAsync(false);
        var result = await _controller.DeleteProduct(1);
        Assert.IsType<NotFoundResult>(result);
    }
}
