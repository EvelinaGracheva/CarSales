using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CarSales.Common.Models;
using CarSales.Data;
using CarSales.Data.Entities;
using CarSales.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarSales.Repository.Implementations
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersRepository> _logger;

        public OrdersRepository(ApplicationDbContext context, IMapper mapper, ILogger<OrdersRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<OrderModel>> GetOrdersListAsync()
        {
                var items = await _context.Orders
                    .ProjectTo<OrderModel>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();

                return items;
        }

        public async Task<OrderModel> GetOrderByIdAsync(int id)
        {
                var item = await _context.Orders
                    .ProjectTo<OrderModel>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == id);

                return item;
        }

        public async Task<OrderModel> CreateOrderAsync(OrderModel model)
        {
            var item = _mapper.Map<Order>(model);

            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<OrderModel> UpdateOrderAsync(OrderModel model)
        {
            var item = await _context.Orders
                .FirstOrDefaultAsync(t => t.Id == model.Id);

            if (item is null)
            {
                _logger.LogWarning($"No Order was Found in Database with Id: {model.Id}");
            }

            _mapper.Map(model, item);

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderModel>(item);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var item = await _context.Orders
                .FirstOrDefaultAsync(t => t. Id == id);

            if (item is null)
            {
                _logger.LogWarning($"No Order was Found in Database with Id: {id}");
                return false;
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

