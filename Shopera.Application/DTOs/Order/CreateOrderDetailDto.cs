namespace Shopera.Application.DTOs.Order
{
    public class CreateOrderDetailDto
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}