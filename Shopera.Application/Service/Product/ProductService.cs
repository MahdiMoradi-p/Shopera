using Shopera.Application.DTOs.Product;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Product;

namespace Shopera.Application.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductService(
            IProductRepository productRepository)
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

                CategoryId = dto.CategoryId,

                CreateDate = DateTime.Now
            };


            var result =
                await _productRepository.AddAsync(product);



            // دوباره از دیتابیس می‌گیریم تا Category و Images لود شوند
            var createdProduct =
                await _productRepository.GetByIdAsync(result.Id);



            return new ProductDto
            {
                Id = createdProduct.Id,

                Title = createdProduct.Title,

                Description = createdProduct.Description,

                Price = createdProduct.Price,

                Stock = createdProduct.Stock,

                CategoryId = createdProduct.CategoryId,

                CategoryName = createdProduct.Category?.Name,

                CreateDate = createdProduct.CreateDate,


                Images = createdProduct.Images
                    .Select(i => new ProductImageDto
                    {
                        Id = i.Id,

                        ImageUrl = i.ImageUrl,

                        ProductId = i.ProductId

                    }).ToList()
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


                CategoryId = x.CategoryId,

                CategoryName = x.Category?.Name,


                CreateDate = x.CreateDate,


                Images = x.Images
                    .Select(i => new ProductImageDto
                    {
                        Id = i.Id,

                        ImageUrl = i.ImageUrl,

                        ProductId = i.ProductId

                    }).ToList()


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


                CategoryId = product.CategoryId,

                CategoryName = product.Category?.Name,


                CreateDate = product.CreateDate,


                Images = product.Images
                    .Select(i => new ProductImageDto
                    {
                        Id = i.Id,

                        ImageUrl = i.ImageUrl,

                        ProductId = i.ProductId

                    }).ToList()
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

            product.CategoryId = dto.CategoryId;



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
        private static ProductDto MapToDto(Domain.Entities.Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                CreateDate = product.CreateDate,

                Images = product.Images.Select(i => new ProductImageDto
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    ProductId = i.ProductId
                }).ToList()
            };
        }

        public async Task<ProductSearchResponseDto> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new ProductSearchResponseDto
                {
                    Message = "لطفاً نام محصول موردنظر را وارد کنید.",
                    HasResults = false,
                    TotalCount = 0
                };
            }

            var products =
                await _productRepository.SearchAsync(query.Trim());

            var productDtos = products
                .Select(MapToDto)
                .ToList();

            if (productDtos.Count == 0)
            {
                return new ProductSearchResponseDto
                {
                    Message = "متأسفیم، محصولی مطابق جست‌وجوی شما پیدا نشد.",
                    HasResults = false,
                    TotalCount = 0
                };
            }

            return new ProductSearchResponseDto
            {
                Message = $"{productDtos.Count} محصول پیدا شد.",
                HasResults = true,
                TotalCount = productDtos.Count,
                Products = productDtos
            };
        }
    }

}