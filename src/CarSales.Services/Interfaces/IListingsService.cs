using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Interfaces
{
    public interface IListingsService
    {
        Task<ActionResult<ListingModel>> CreateAsync(ListingModel model);
        Task<bool> DeleteByIdAsync(int id);
        Task<ListingModel> GetByIdAsync(int id);
        Task<List<ListingModel>> AllAsync();
        Task<ActionResult<ListingModel>> UpdateByIdAsync(int id, ListingModel model);
    }
}
