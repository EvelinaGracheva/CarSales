using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Data.Entities;
using CarSales.Managers;
using CarSales.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarsManager _carsManager;

        public CarsController(ICarsManager carsManager)
        {
            _carsManager = carsManager;
        }

        [HttpGet]
        public async Task<List<Car>> GetCarsList()
        {
            var items = await _carsManager.GetCarsListAsync();

            return items;
        }

        [HttpGet("GetByCarNumber")]
        public async Task<Car> GetCarCarNumber(string carNumber)
        {
            var item = await _carsManager.GetCarCarNumberByAsync(carNumber);

            return item;
        }

        [HttpPost]
        public async Task<Car> CreateCarAsync(Car car)
        {
            var createItem = await _carsManager.CreateCarAsync(car);

            return createItem;
        }

        [HttpPut]
        public async Task<Car> UpdateCarAsync(Car car)
        {
            var updateItem = await _carsManager.UpdateCarAsync(car);

            return updateItem;
        }

        [HttpDelete]
        public async Task<bool> DeleteCarAsync(string carNumber)
        {
            return await _carsManager.DeleteCarAsync(carNumber);
        }
    }
}