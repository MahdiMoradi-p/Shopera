using Shopera.Application.DTOs.Product;

namespace Shopera.Application.IService.Product
{
    public interface IProductDetailService
    {
        Task<ProductDetailDto> CreateAsync(
            CreateProductDetailDto dto);

        Task<List<ProductDetailDto>> GetAllAsync();

        Task<ProductDetailDto?> GetByIdAsync(int id);

        Task<List<ProductDetailDto>> GetByProductIdAsync(
            int productId);

        Task<bool> UpdateAsync(
            UpdateProductDetailDto dto);

        Task<bool> DeleteAsync(int id);
    }
}