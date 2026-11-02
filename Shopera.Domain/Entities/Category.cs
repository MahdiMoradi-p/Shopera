namespace Shopera.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}
