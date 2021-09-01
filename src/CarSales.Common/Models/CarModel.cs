﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Common.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string CarNumber { get; set; }

        [Required]
        public DateTime ManufactureYear { get; set; }

        [Required]
        public string VinCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime SaleStartDate { get; set; }

        [Required]
        public DateTime SaleEndDate { get; set; }
    }
}