using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

namespace CarSales.Repository.Interfaces
{
    public interface ICarsRepository
    {
        Task<CarModel> CreateCarAsync(CarModel model);
        Task<bool> DeleteCarAsync(string carNumber);
        Task<CarModel> GetCarByCarNumberAsync(string carNumber);
        Task<List<CarModel>> GetCarsListAsync();
        Task<CarModel> UpdateCarAsync(CarModel model);
    }
}
