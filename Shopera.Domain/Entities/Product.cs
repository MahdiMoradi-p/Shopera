namespace Shopera.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ICollection<ProductDetail> ProductDetails { get; set; }
    = new List<ProductDetail>();
        public ICollection<OrderDetail> OrderDetails { get; set; }
    = new List<OrderDetail>();
    }
}