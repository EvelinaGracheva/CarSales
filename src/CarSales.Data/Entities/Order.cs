using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal Price { get; set; }
    }
}
