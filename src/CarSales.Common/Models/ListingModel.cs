using System;

namespace CarSales.Common.Models
{
    public class ListingModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public int ClientId { get; set; }

        public int VehicleId { get; set; }

        public int? PurchaseId { get; set; }
    }
}
