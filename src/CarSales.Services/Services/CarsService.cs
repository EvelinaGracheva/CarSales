using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;
using CarSales.Repository.Interfaces;
using CarSales.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _carsService;

        public CarsService(ICarsRepository carsRepository)
        {
            _carsService = carsRepository;
        }

        public async Task<List<CarModel>> GetCarsList()
        {
            var items = await _carsService.GetCarsListAsync();

            return items;
        }

        public async Task<CarModel> GetCarbyCarNumber(string carNumber)
        {
            var item = await _carsService.GetCarByCarNumberAsync(carNumber);

            return item;
        }

        public async Task<ActionResult<CarModel>> CreateCarAsync(CarModel model)
        {
            var createItem = await _carsService.CreateCarAsync(model);

            return createItem;
        }

        public async Task<ActionResult<CarModel>> UpdateCarAsync(CarModel model)
        {
            var updateItem = await _carsService.UpdateCarAsync(model);

            return updateItem;
        }

        public async Task<bool> DeleteCarAsync(string carNumber)
        {
            return await _carsService.DeleteCarAsync(carNumber);
        }
    }
}
