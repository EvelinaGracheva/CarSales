using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;
using CarSales.Repository.Interfaces;
using CarSales.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<OrderModel>> GetOrdersListAsync()
        {
            var items = await _ordersRepository.GetOrdersListAsync();

            return items;
        }

        public async Task<OrderModel> GetOrderByIdAsync(int id)
        {
            var item = await _ordersRepository.GetOrderByIdAsync(id);

            return item;
        }

        public async Task<ActionResult<OrderModel>> CreateOrderAsync(OrderModel model)
        {
            var createItem = await _ordersRepository.CreateOrderAsync(model);

            return createItem;
        }

        public async Task<ActionResult<OrderModel>> UpdateOrderAsync(OrderModel model)
        {
            var updateItem = await _ordersRepository.UpdateOrderAsync(model);

            return updateItem;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _ordersRepository.DeleteOrderAsync(id);
        }
    }
}
