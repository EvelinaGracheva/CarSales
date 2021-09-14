using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Interfaces
{
    public interface IVehiclesService
    {
        Task<VehicleModel> CreateAsync(VehicleModel model);
        Task<bool> DeleteByVinCodeAsync(string vinCode);
        Task<VehicleModel?> GetByVinCodeAsync(string vinCode);
        Task<List<VehicleModel>> AllAsync();
        Task<VehicleModel?> UpdateByVinCodeAsync(string vinCode, VehicleModel model);
    }
}
