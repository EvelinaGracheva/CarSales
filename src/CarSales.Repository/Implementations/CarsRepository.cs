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
    public class CarsRepository : ICarsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CarsRepository> _logger;

        public CarsRepository(ApplicationDbContext context, IMapper mapper, ILogger<CarsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CarModel>> GetCarsListAsync()
        {
            var items = await _context.Cars
                .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return items;
        }

        public async Task<CarModel> GetCarByCarNumberAsync(string carNumber)
        {
            var item = await _context.Cars
                .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.CarNumber == carNumber);

            return item;
        }

        public async Task<CarModel> CreateCarAsync(CarModel model)
        {
            var item = _mapper.Map<Car>(model);

            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<CarModel> UpdateCarAsync(CarModel model)
        {
            var item = await _context.Cars
                .FirstOrDefaultAsync(t => t.CarNumber == model.CarNumber);

            if (item is null)
            {
                _logger.LogWarning($"No Car was Found in Database with CarNumber: {model.CarNumber}");
            }

            _mapper.Map(model, item);

            await _context.SaveChangesAsync();

            return _mapper.Map<CarModel>(item);
        }

        public async Task<bool> DeleteCarAsync(string carNumber)
        {
            var item = await _context.Cars
                .Include(t => t.Orders)
                .FirstOrDefaultAsync(t => t.CarNumber == carNumber);

            if (item is null)
            {
                _logger.LogWarning($"No Car was Found in Database with CarNumber: {carNumber}");
                return false;
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

