namespace Shopera.Application.DTOs.Product
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int ProductId { get; set; }
    }
}
