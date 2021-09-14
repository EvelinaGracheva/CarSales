using System;
using System.ComponentModel.DataAnnotations;

using CarSales.Data.Enums;

namespace CarSales.Common.Models
{
    public class VehicleModel
    {
        public int Year { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Number { get; set; } = null!;

        public string VinCode { get; set; } = null!;

        public VehicleType VehicleType { get; set; }
    }
}
