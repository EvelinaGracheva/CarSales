using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

namespace CarSales.Repository.Interfaces
{
    public interface IVehiclesRepository
    {
        Task<VehicleModel> CreateAsync(VehicleModel model);
        Task<bool> DeleteByVinCodeAsync(string vinCode);
        Task<VehicleModel?> GetByVinCodeAsync(string vinCode);
        Task<List<VehicleModel>> AllAsync();
        Task<VehicleModel?> UpdateByVinCodeAsync(string vinCode, VehicleModel model);
        Task<bool> IsVinCodeExistsAsync(string vinCode);
    }
}
