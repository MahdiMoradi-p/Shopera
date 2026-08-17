using Shopera.Application.DTOs.Product;

namespace Shopera.Application.IService.Product
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateProductDto dto);

        Task<List<ProductDto>> GetAllAsync();

        Task<ProductDto?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(UpdateProductDto dto);

        Task<bool> DeleteAsync(int id);
    }
}