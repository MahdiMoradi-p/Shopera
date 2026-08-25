using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopera.Domain.Entities
{
     public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();

    }
}
