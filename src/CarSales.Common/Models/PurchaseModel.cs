using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Common.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public List<ListingModel> Listings { get; set; } = new();
    }
}
