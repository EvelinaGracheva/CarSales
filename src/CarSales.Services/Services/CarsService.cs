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
        private readonly ICarsRepository _carsRepository;

        public CarsService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public async Task<List<CarModel>> GetCarsListAsync()
        {
            var items = await _carsRepository.GetCarsListAsync();

            return items;
        }

        public async Task<CarModel> GetCarbyCarNumberAsync(string carNumber)
        {
            var item = await _carsRepository.GetCarByCarNumberAsync(carNumber);

            return item;
        }

        public async Task<ActionResult<CarModel>> CreateCarAsync(CarModel model)
        {
            var createItem = await _carsRepository.CreateCarAsync(model);

            return createItem;
        }

        public async Task<ActionResult<CarModel>> UpdateCarAsync(CarModel model)
        {
            var updateItem = await _carsRepository.UpdateCarAsync(model);

            return updateItem;
        }

        public async Task<bool> DeleteCarAsync(string carNumber)
        {
            return await _carsRepository.DeleteCarAsync(carNumber);
        }
    }
}
