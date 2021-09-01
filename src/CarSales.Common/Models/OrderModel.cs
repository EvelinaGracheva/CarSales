using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Common.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int ClientId { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal Price { get; set; }
    }
}
