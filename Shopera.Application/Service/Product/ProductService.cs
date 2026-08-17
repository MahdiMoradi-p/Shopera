using Shopera.Application.DTOs.Product;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Product;

namespace Shopera.Application.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateAsync(
            CreateProductDto dto)
        {
            var product = new Domain.Entities.Product
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CreateDate = DateTime.Now
            };

            var result =
                await _productRepository.AddAsync(product);

            return new ProductDto
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Price = result.Price,
                Stock = result.Stock,
                CreateDate = result.CreateDate
            };
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products =
                await _productRepository.GetAllAsync();

            return products.Select(x => new ProductDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                CreateDate = x.CreateDate
            }).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product =
                await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreateDate = product.CreateDate
            };
        }

        public async Task<bool> UpdateAsync(
            UpdateProductDto dto)
        {
            var product =
                await _productRepository.GetByIdAsync(dto.Id);

            if (product == null)
                return false;

            product.Title = dto.Title;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            await _productRepository.UpdateAsync(product);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product =
                await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            await _productRepository.DeleteAsync(product);

            return true;
        }
    }
}