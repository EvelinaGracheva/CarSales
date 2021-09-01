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
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrdersService ordersService, ILogger<OrdersController> logger)
        {
            _ordersService = ordersService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<OrderModel>> GetOrdersList()
        {
            try
            {
                var items = await _ordersService.GetOrdersListAsync();

                if (items.Count == 0)
                {
                    _logger.LogWarning($"No Orders were Found in Database");
                }
                return items;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<OrderModel>().ToList();
            }
        }

        [HttpGet("GetById")]
        public async Task<OrderModel> GetorderByIdAsync(int id)
        {
            try
            {
                var item = await _ordersService.GetOrderByIdAsync(id);

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
        public async Task<ActionResult<OrderModel>> CreateOrder(OrderModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }
            try
            {
                var createItem = await _ordersService.CreateOrderAsync(model);

                return createItem;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpPut]
        public async Task<ActionResult<OrderModel>> UpdateOrder(OrderModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            try
            {
                var updateItem = await _ordersService.UpdateOrderAsync(model);

                return updateItem;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpDelete]
        public async Task<bool> DeleteOrder(int id)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = await _ordersService.DeleteOrderAsync(id);

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
