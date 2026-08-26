namespace Shopera.Application.DTOs.Invoice
{
    public class CreateInvoiceDetailDto
    {
        public int InvoiceId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}