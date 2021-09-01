using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Data.Entities
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string CarNumber { get; set; }

        public DateTime ManufactureYear { get; set; }

        [Required]
        public string VinCode { get; set; }

        public decimal Price { get; set; }

        public DateTime SaleStartDate { get; set; }

        public DateTime SaleEndDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
