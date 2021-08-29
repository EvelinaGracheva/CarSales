﻿using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace CarSales.Common.Models
{
    public class ClientModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\d{11}", ErrorMessage = "Personal number must be 11 digits length")]
        public string PersonalNumber { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }


    }
}
