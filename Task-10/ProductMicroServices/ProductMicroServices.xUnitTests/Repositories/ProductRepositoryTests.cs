using Xunit;
using ProductMicroServices.Data;
using ProductMicroServices.Models;
using ProductMicroServices.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ProductRepositoryTests
{
    private readonly ProductRepository _repository;
    private readonly ApplicationDbContext _context;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ProductRepository(_context);
    }

    [Fact]
    public async Task GetProductById_ReturnsCorrectProduct()
    {
        var product = new Product
        {
            Id = 1,
            Name = "Test Pen",
            Category = "Stationery",
            Price = 15,
            Stock = 100
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = await _repository.GetProductByIdAsync(product.Id);

        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
        Assert.Equal(product.Name, result.Name);
    }

    [Fact]
    public async Task GetProductById_ReturnsNullWhenNotFound()
    {
        var result = await _repository.GetProductByIdAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task AddProductAsync_AddsSuccessfully()
    {
        var product = new Product
        {
            Id = 2,
            Name = "Pencil",
            Category = "Stationery",
            Price = 5,
            Stock = 200
        };

        var result = await _repository.AddProductAsync(product);

        Assert.NotNull(result);
        Assert.Equal("Pencil", result.Name);
    }

    [Fact]
    public async Task UpdateProductAsync_UpdatesCorrectly()
    {
        var originalProduct = new Product
        {
            Id = 3,
            Name = "Pencil",
            Category = "Stationery",
            Price = 5,
            Stock = 200
        };

        _context.Products.Add(originalProduct);
        await _context.SaveChangesAsync();

        var updatedProduct = new Product
        {
            Id = 3,
            Name = "Apsara Pencil",
            Category = "Stationery",
            Price = 5,
            Stock = 200
        };

        var result = await _repository.UpdateProductAsync(updatedProduct);

        Assert.NotNull(result);
        Assert.Equal("Apsara Pencil", result.Name);
        Assert.Equal(updatedProduct.Category, result.Category);
    }

    [Fact]
    public async Task DeleteProductAsync_DeletesCorrectly()
    {
        var product = new Product
        {
            Id = 5,
            Name = "Pencil",
            Category = "Stationery",
            Price = 5,
            Stock = 200
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = await _repository.DeleteProductAsync(product.Id);
        var deletedProduct = await _repository.GetProductByIdAsync(product.Id);

        Assert.True(result);
        Assert.Null(deletedProduct);
    }
}
