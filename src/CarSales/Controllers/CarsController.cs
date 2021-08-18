using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<CarModel>> GetCarsList()
        {
            var items = await _carsManager.GetCarsListAsync();

            return items;
        }

        [HttpGet("GetByCarNumber")]
        public async Task<CarModel> GetCarCarNumber(string carNumber)
        {
            var item = await _carsManager.GetCarCarNumberByAsync(carNumber);

            return item;
        }

        [HttpPost]
        public async Task<CarModel> CreateCarAsync(CarModel model)
        {
            var createItem = await _carsManager.CreateCarAsync(model);

            return createItem;
        }

        [HttpPut]
        public async Task<CarModel> UpdateCarAsync(CarModel model)
        {
            var updateItem = await _carsManager.UpdateCarAsync(model);

            return updateItem;
        }

        [HttpDelete]
        public async Task<bool> DeleteCarAsync(string carNumber)
        {
            return await _carsManager.DeleteCarAsync(carNumber);
        }
    }
}