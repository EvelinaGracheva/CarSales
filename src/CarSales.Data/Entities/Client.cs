using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace CarSales.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ModifiedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string PersonalNumber { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        public string? Email { get; set; }

        public virtual ICollection<Listing> Listings { get; set; } = new HashSet<Listing>();

        public virtual ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
    }
}
