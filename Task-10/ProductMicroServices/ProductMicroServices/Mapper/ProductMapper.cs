using ProductMicroServices.DTOs;
using ProductMicroServices.Models;

namespace ProductMicroServices.Mappers
{
    public static class ProductMapper
    {
        public static Product MapToEntity(ProductCreateDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }
    }
}
