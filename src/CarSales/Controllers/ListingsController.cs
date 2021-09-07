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
        public async Task<List<ListingModel>> All()
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
                return Enumerable.Empty<ListingModel>().ToList();
            }
        }

        [HttpGet("{id}")]
        public async Task<ListingModel> GetById(int id)
        {
            try
            {
                var item = await _listingsService.GetByIdAsync(id);

                if (item is null)
                {
                    _logger.LogWarning($"No Order was Found in Database with Id: {id}");
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
                return null;
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

                return updateItem;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(int id)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = await _listingsService.DeleteByIdAsync(id);

                if (!isDeleted)
                {
                    _logger.LogWarning($"No Order was Found in Database with Id: {id}");
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
