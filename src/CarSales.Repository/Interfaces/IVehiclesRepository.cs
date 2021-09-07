using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

namespace CarSales.Repository.Interfaces
{
    public interface IVehiclesRepository
    {
        Task<VehicleModel> CreateAsync(VehicleModel model);
        Task<bool> DeleteByVinCodeAsync(string number);
        Task<VehicleModel> GetByVinCodeAsync(string number);
        Task<List<VehicleModel>> AllAsync();
        Task<VehicleModel> UpdateByVinCodeAsync(string number, VehicleModel model);
    }
}
