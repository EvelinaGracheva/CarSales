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
    public class ListingsController : ControllerBase
    {
        private readonly IListingsService _listingsService;
        private readonly ILogger<ListingsController> _logger;

        public ListingsController(IListingsService listingsService, ILogger<ListingsController> logger)
        {
            _listingsService = listingsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListingModel>>> All()
        {
            try
            {
                var items = await _listingsService.AllAsync();

                if (items.Count == 0)
                {
                    _logger.LogWarning($"No Orders were Found in Database");
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListingModel>> GetById(int id)
        {
            try
            {
                var item = await _listingsService.GetByIdAsync(id);

                if (item is null)
                {
                    _logger.LogWarning($"No Order was Found in Database with Id: {id}");
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
        public async Task<ActionResult<ListingModel>> Create(ListingModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }
            try
            {
                var createItem = await _listingsService.CreateAsync(model);

                return createItem;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ListingModel>> UpdateById(int id, ListingModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            try
            {
                var updateItem = await _listingsService.UpdateByIdAsync(id, model);

                if (updateItem is null)
                {
                    _logger.LogWarning($"No Order was Found in Database with Id: {id}");
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteById(int id)
        {
            try
            {
                bool isDeleted = await _listingsService.DeleteByIdAsync(id);

                if (!isDeleted)
                {
                    _logger.LogWarning($"No Order was Found in Database with Id: {id}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

            return true;
        }
    }
}
