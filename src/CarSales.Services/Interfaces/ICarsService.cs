using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Interfaces
{
    public interface ICarsService
    {
        Task<ActionResult<CarModel>> CreateCarAsync(CarModel model);
        Task<bool> DeleteCarAsync(string carNumber);
        Task<CarModel> GetCarbyCarNumberAsync(string carNumber);
        Task<List<CarModel>> GetCarsListAsync();
        Task<ActionResult<CarModel>> UpdateCarAsync(CarModel model);
    }
}
