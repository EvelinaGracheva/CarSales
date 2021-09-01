using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<ActionResult<OrderModel>> CreateOrderAsync(OrderModel model);
        Task<bool> DeleteOrderAsync(int id);
        Task<OrderModel> GetOrderByIdAsync(int id);
        Task<List<OrderModel>> GetOrdersListAsync();
        Task<ActionResult<OrderModel>> UpdateOrderAsync(OrderModel model);
    }
}
