namespace Shopera.Application.DTOs.Product
{
    public class ProductSearchResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public bool HasResults { get; set; }
        public int TotalCount { get; set; }
        public List<ProductDto> Products { get; set; } = new();
    }
}
