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
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<VehiclesRepository> _logger;

        public VehiclesRepository(ApplicationDbContext context, IMapper mapper, ILogger<VehiclesRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<VehicleModel>> AllAsync()
        {
            var items = await _context.Vehicles
                .ProjectTo<VehicleModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return items;
        }

        public async Task<VehicleModel> GetByVinCodeAsync(string vinCode)
        {
            var item = await _context.Vehicles
                .ProjectTo<VehicleModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.VinCode == vinCode);

            return item;
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel model)
        {
            model.Id = 0;

            var item = _mapper.Map<Vehicle>(model);

            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<VehicleModel> UpdateByVinCodeAsync(string vinCode, VehicleModel model)
        {
            var item = await _context.Vehicles
                .FirstOrDefaultAsync(t => t.VinCode == vinCode);

            if (item is null)
            {
                _logger.LogWarning($"No Car was Found in Database with Vin Code: {vinCode}");
            }

            _mapper.Map(model, item);
            item.ModifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleModel>(item);
        }

        public async Task<bool> DeleteByVinCodeAsync(string vinCode)
        {
            var item = await _context.Vehicles
                .FirstOrDefaultAsync(t => t.VinCode == vinCode);

            if (item is null)
            {
                _logger.LogWarning($"No Car was Found in Database with Vin Code: {vinCode}");
                return false;
            }

            item.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

