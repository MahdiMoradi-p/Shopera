using Microsoft.AspNetCore.Identity;

namespace Shopera.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ICollection<Order> Orders { get; set; }
    = new List<Order>();
    }
}