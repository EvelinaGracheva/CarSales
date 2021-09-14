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
        public async Task<ActionResult<List<VehicleModel>>> All()
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{vinCode}")]
        public async Task<ActionResult<VehicleModel>> GetByVinCode(string vinCode)
        {
            try
            {
                var item = await _vehiclesService.GetByVinCodeAsync(vinCode);

                if (item is null)
                {
                    _logger.LogWarning($"No Car was Found in Database with Vin Code: {vinCode}");
                    return NotFound();
                }

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
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

                if (updateItem is null)
                {
                    _logger.LogWarning($"No Car was Found in Database with Vin Code: {vinCode}");
                    return NotFound();
                }

                return updateItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{vinCode}")]
        public async Task<ActionResult<bool>> DeleteByVinCode(string vinCode)
        {
            try
            {
                bool isDeleted = await _vehiclesService.DeleteByVinCodeAsync(vinCode);

                if (!isDeleted)
                {
                    _logger.LogWarning($"No Car was Found in Database with Vin Code: {vinCode}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                return BadRequest(ex.Message);
            }

            return true;
        }
    }
}
