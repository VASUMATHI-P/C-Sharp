using Microsoft.AspNetCore.Mvc;
using ProductMicroServices.Models;

namespace ProductMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        // GET: api/products/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            var createdProduct = await _service.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest();

            var updatedProduct = await _service.UpdateProductAsync(product);
            if (updatedProduct == null)
                return NotFound();
            return Ok(updatedProduct);
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _service.DeleteProductAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
