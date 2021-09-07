using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Entities
{
    public class Purchase
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ModifiedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int ClientId { get; set; }

        public virtual Client? Client { get; set; }

        public virtual ICollection<Listing> Listings { get; set; } = new HashSet<Listing>();
    }
}
