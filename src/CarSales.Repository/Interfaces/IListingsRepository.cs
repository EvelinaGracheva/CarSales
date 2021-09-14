using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

namespace CarSales.Repository.Interfaces
{
    public interface IListingsRepository
    {
        Task<ListingModel> CreateAsync(ListingModel model);
        Task<bool> DeleteByIdAsync(int id);
        Task<ListingModel?> GetByIdAsync(int id);
        Task<List<ListingModel>> AllAsync();
        Task<ListingModel?> UpdateByIdAsync(int id, ListingModel model);
    }
}
