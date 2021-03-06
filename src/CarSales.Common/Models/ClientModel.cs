using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Common.Models
{
    public class ClientModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [RegularExpression(@"\d{11}", ErrorMessage = "Personal number must be 11 digits length")]
        public string PersonalNumber { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        public string? Email { get; set; }
    }
}
