using System;
using System.ComponentModel.DataAnnotations;

using CarSales.Data.Enums;

namespace CarSales.Common.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ModifiedAt { get; set; }

        public DateTime? DeletedAt { get; set; }


        public int Year { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Number { get; set; } = null!;

        public string VinCode { get; set; } = null!;

        public VehicleType VehicleType { get; set; }
    }
}
