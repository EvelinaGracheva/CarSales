using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CarSales.Data;
using CarSales.Data.Entities;
using CarSales.Models;

using Microsoft.EntityFrameworkCore;

namespace CarSales.Managers
{
    public class CarsManager: ICarsManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarsManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CarModel>> GetCarsListAsync()
        {
            var items = await _context.Cars
                .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return items;
        }

        public async Task<CarModel> GetCarCarNumberByAsync(string carNumber)
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
                .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.CarNumber == model.CarNumber);

            if (item is null)
            {
                return null;
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
            var item = await _context.Cars
                .ProjectTo<CarModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.CarNumber == carNumber);

            if (item is null)
            {
                return false;
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }

    public interface ICarsManager
    {
        Task<CarModel> CreateCarAsync(CarModel model);
        Task<bool> DeleteCarAsync(string carNumber);
        Task<CarModel> GetCarCarNumberByAsync(string carNumber);
        Task<List<CarModel>> GetCarsListAsync();
        Task<CarModel> UpdateCarAsync(CarModel model);
    }
}

