using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

namespace CarSales.Repository.Interfaces
{
    public interface IOrdersRepository
    {
        Task<OrderModel> CreateOrderAsync(OrderModel model);
        Task<bool> DeleteOrderAsync(int id);
        Task<OrderModel> GetOrderByIdAsync(int id);
        Task<List<OrderModel>> GetOrdersListAsync();
        Task<OrderModel> UpdateOrderAsync(OrderModel model);
    }
}
