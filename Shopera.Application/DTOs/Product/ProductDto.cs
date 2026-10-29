namespace Shopera.Application.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public int? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public DateTime CreateDate { get; set; }
        public List<ProductImageDto> Images { get; set; }
                    = new List<ProductImageDto>();
    }
}