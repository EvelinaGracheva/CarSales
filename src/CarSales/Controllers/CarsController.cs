using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarSales.Common.Models;
using CarSales.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarsService _carsService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarsService carsService, ILogger<CarsController> logger)
        {
            _carsService = carsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<CarModel>> GetCarsList()
        {
            var items = await _carsService.GetCarsList();

            return items;
        }

        [HttpGet("GetByCarNumber")]
        public async Task<CarModel> GetCarByCarNumber(string carNumber)
        {
            var item = await _carsService.GetCarbyCarNumber(carNumber);

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<CarModel>> CreateCar(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            var createItem = await _carsService.CreateCarAsync(model);

            return createItem;
        }

        [HttpPut]
        public async Task<ActionResult<CarModel>> UpdateCar(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }
            var updateItem = await _carsService.UpdateCarAsync(model);

            return updateItem;
        }

        [HttpDelete]
        public async Task<bool> DeleteCar(string carNumber)
        {
            return await _carsService.DeleteCarAsync(carNumber);
        }
    }
}