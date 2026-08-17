namespace Shopera.Application.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreateDate { get; set; }
    }
}