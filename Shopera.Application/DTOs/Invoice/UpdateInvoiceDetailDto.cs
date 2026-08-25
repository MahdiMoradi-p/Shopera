namespace Shopera.Application.DTOs.Invoice
{
    public class UpdateInvoiceDetailDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}