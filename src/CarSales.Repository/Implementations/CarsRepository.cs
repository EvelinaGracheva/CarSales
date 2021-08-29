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
            try
            {
                var items = await _context.Cars
                    .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();

                if (items.Count == 0)
                {
                    _logger.LogWarning($"No Cars were Found in Database");
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<CarModel>().ToList();
            }
        }

        public async Task<CarModel> GetCarByCarNumberAsync(string carNumber)
        {
            try
            {
                var item = await _context.Cars
                    .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.CarNumber == carNumber);

                if (item is null)
                {
                    _logger.LogWarning($"No Car was Found in Database with CarNumber: {carNumber}");
                }

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
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
                .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.CarNumber == model.CarNumber);

            if (item is null)
            {
                _logger.LogWarning($"No Car was Found in Database with CarNumber: {model.CarNumber}");
            }

            item.Make = model.Make;
            item.Model = model.Model;
            item.Price = model.Price;
            item.CarNumber = model.CarNumber;
            item.ManufactureYear = model.ManufactureYear;
            item.VinCode = model.VinCode;
            item.SaleStartDate = model.SaleStartDate;
            item.SaleEndDate = model.SaleEndDate;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteCarAsync(string carNumber)
        {
            _logger.LogInformation("CarsManager - DeleteCarAsync - Started");

            var item = await _context.Cars
                .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.CarNumber == carNumber);

            if (item is null)
            {
                _logger.LogWarning($"No Car was Found in Database with CarNumber: {carNumber}");
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();

            _logger.LogInformation("CarsManager - DeleteCarAsync - Ended");

            return true;
        }
    }
}

