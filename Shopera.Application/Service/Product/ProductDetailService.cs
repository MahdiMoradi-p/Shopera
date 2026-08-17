using Shopera.Application.DTOs.Product;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Product;

namespace Shopera.Application.Service.Product
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IProductDetailRepository _detailRepository;
        private readonly IProductRepository _productRepository;

        public ProductDetailService(
            IProductDetailRepository detailRepository,
            IProductRepository productRepository)
        {
            _detailRepository = detailRepository;
            _productRepository = productRepository;
        }

        public async Task<ProductDetailDto> CreateAsync(
            CreateProductDetailDto dto)
        {
            var product =
                await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            var detail = new Domain.Entities.ProductDetail
            {
                ProductId = dto.ProductId,
                Key = dto.Key,
                Value = dto.Value
            };

            var result =
                await _detailRepository.AddAsync(detail);

            return new ProductDetailDto
            {
                Id = result.Id,
                ProductId = result.ProductId,
                Key = result.Key,
                Value = result.Value
            };
        }

        public async Task<List<ProductDetailDto>> GetAllAsync()
        {
            var details =
                await _detailRepository.GetAllAsync();

            return details.Select(x => new ProductDetailDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Key = x.Key,
                Value = x.Value
            }).ToList();
        }

        public async Task<ProductDetailDto?> GetByIdAsync(int id)
        {
            var detail =
                await _detailRepository.GetByIdAsync(id);

            if (detail == null)
                return null;

            return new ProductDetailDto
            {
                Id = detail.Id,
                ProductId = detail.ProductId,
                Key = detail.Key,
                Value = detail.Value
            };
        }

        public async Task<List<ProductDetailDto>> GetByProductIdAsync(
            int productId)
        {
            var product =
                await _productRepository.GetByIdAsync(productId);

            if (product == null)
                throw new Exception("Product not found.");

            var details =
                await _detailRepository.GetByProductIdAsync(productId);

            return details.Select(x => new ProductDetailDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Key = x.Key,
                Value = x.Value
            }).ToList();
        }

        public async Task<bool> UpdateAsync(
            UpdateProductDetailDto dto)
        {
            var detail =
                await _detailRepository.GetByIdAsync(dto.Id);

            if (detail == null)
                return false;

            var product =
                await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            detail.ProductId = dto.ProductId;
            detail.Key = dto.Key;
            detail.Value = dto.Value;

            await _detailRepository.UpdateAsync(detail);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var detail =
                await _detailRepository.GetByIdAsync(id);

            if (detail == null)
                return false;

            await _detailRepository.DeleteAsync(detail);

            return true;
        }
    }
}