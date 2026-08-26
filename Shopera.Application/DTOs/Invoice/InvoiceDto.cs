using Shopera.Domain.Enums;

namespace Shopera.Application.DTOs.Invoice
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime InvoiceDate { get; set; }

        public InvoiceStatus Status { get; set; }
    }
}