using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarSales.Managers;
using CarSales.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarsManager _carsManager;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarsManager carsManager, ILogger<CarsController> logger)
        {
            _carsManager = carsManager;
            _logger = logger;
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
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            var createItem = await _carsManager.CreateCarAsync(model);

            return createItem;
        }

        [HttpPut]
        public async Task<CarModel> UpdateCarAsync(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }
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