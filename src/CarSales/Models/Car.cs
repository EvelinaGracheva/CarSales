using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Models
{
    public class Car

    {   [Required]
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
