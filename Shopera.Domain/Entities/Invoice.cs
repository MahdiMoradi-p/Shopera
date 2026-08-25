using Shopera.Domain.Enums;

namespace Shopera.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime InvoiceDate { get; set; }

        public InvoiceStatus Status { get; set; }

        public Order Order { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
            = new List<InvoiceDetail>();
    }
}