namespace Shopera.Application.DTOs.Order
{
    public class OrderDetailDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}