using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarSales.Data;
using CarSales.Models;

using Microsoft.EntityFrameworkCore;

namespace CarSales.Managers
{
    public class CarsManager: ICarsManager
    {
        private readonly ApplicationDbContext _context;

        public CarsManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetCarsListAsync()
        {
            var items = await _context.Cars.AsNoTracking().ToListAsync();

            return items;
        }

        public async Task<Car> GetCarCarNumberByAsync(string carNumber)
        {
            var item = await _context.Cars.AsNoTracking().FirstOrDefaultAsync(t => t.CarNumber == carNumber);

            return item;
        }

        public async Task<Car> CreateCarAsync(Car car)
        {
            car.Id = 0;
            await _context.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            var item = await _context.Cars.FirstOrDefaultAsync(t => t.CarNumber == car.CarNumber);

            if (item is null)
            {
                return null;
            }

            item.Make = car.Make;
            item.Model = car.Model;
            item.Price = car.Price;
            item.CarNumber = car.CarNumber;
            item.ManufactureYear = car.ManufactureYear;
            item.VinCode = car.VinCode;
            item.SaleStartDate = car.SaleStartDate;
            item.SaleEndDate = car.SaleEndDate;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteCarAsync(string carNumber)
        {
            var item = await _context.Cars.FirstOrDefaultAsync(t => t.CarNumber == carNumber);

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
        Task<Car> CreateCarAsync(Car car);
        Task<bool> DeleteCarAsync(string carNumber);
        Task<Car> GetCarCarNumberByAsync(string carNumber);
        Task<List<Car>> GetCarsListAsync();
        Task<Car> UpdateCarAsync(Car car);
    }
}

