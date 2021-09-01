﻿using System;
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
            try
            {
                var items = await _carsService.GetCarsListAsync();

                if (items.Count == 0)
                {
                    _logger.LogWarning($"No Cars were Found in Database");
                }
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<CarModel>().ToList();
            }
        }

        [HttpGet("GetByCarNumber")]
        public async Task<CarModel> GetCarByCarNumber(string carNumber)
        {
            try
            {
                var item = await _carsService.GetCarbyCarNumberAsync(carNumber);

                if (item is null)
                {
                    _logger.LogWarning($"No Car was Found in Database with CarNumber: {carNumber}");
                }

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<CarModel>> CreateCar(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            try
            {
                var createItem = await _carsService.CreateCarAsync(model);

                return createItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpPut]
        public async Task<ActionResult<CarModel>> UpdateCar(CarModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            try
            {
                var updateItem = await _carsService.UpdateCarAsync(model);

                return updateItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpDelete]
        public async Task<bool> DeleteCar(string carNumber)
        {
            bool isDeleted = false;

            try
            {
                isDeleted = await _carsService.DeleteCarAsync(carNumber);

                if (!isDeleted)
                {
                    _logger.LogWarning($"No Car was Found in Database with CarNumber: {carNumber}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
            }

            return isDeleted;
        }
    }
}
