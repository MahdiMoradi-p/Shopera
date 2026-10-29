namespace Shopera.Application.DTOs.Buy
{
    public class CheckoutResultDto
    {
        public int OrderId { get; set; }
        public int InvoiceId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}