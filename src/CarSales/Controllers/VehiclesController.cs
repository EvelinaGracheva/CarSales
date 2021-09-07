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
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesService _vehiclesService;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(IVehiclesService vehiclesService, ILogger<VehiclesController> logger)
        {
            _vehiclesService = vehiclesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<VehicleModel>> All()
        {
            try
            {
                var items = await _vehiclesService.AllAsync();

                if (items.Count == 0)
                {
                    _logger.LogWarning($"No Cars were Found in Database");
                }
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<VehicleModel>().ToList();
            }
        }

        [HttpGet("{vinCode}")]
        public async Task<VehicleModel> GetByVinCode(string vinCode)
        {
            try
            {
                var item = await _vehiclesService.GetByVinCodeAsync(vinCode);

                if (item is null)
                {
                    _logger.LogWarning($"No Car was Found in Database with Vin Code: {vinCode}");
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
        public async Task<ActionResult<VehicleModel>> Create(VehicleModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            try
            {
                var createItem = await _vehiclesService.CreateAsync(model);

                return createItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpPut("{vinCode}")]
        public async Task<ActionResult<VehicleModel>> UpdateByVinCode(string vinCode, VehicleModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            try
            {
                var updateItem = await _vehiclesService.UpdateByVinCodeAsync(vinCode, model);

                return updateItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpDelete("{vinCode}")]
        public async Task<bool> DeleteByVinCode(string vinCode)
        {
            bool isDeleted = false;

            try
            {
                isDeleted = await _vehiclesService.DeleteByVinCodeAsync(vinCode);

                if (!isDeleted)
                {
                    _logger.LogWarning($"No Car was Found in Database with Vin Code: {vinCode}");
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
