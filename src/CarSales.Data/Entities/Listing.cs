using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarSales.Data.Enums;

namespace CarSales.Data.Entities
{
    public class Listing
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ModifiedAt { get; set; }

        public DateTime? DeletedAt { get; set; }


      
        public decimal Price { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }


        public int ClientId { get; set; }

        public virtual Client? Client { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle? Vehicle { get; set; }

        public int? PurchaseId { get; set; }

        public virtual Purchase? Purchase { get; set; }
    }
}
