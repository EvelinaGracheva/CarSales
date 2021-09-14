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
    public class ListingsRepository : IListingsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ListingsRepository> _logger;

        public ListingsRepository(ApplicationDbContext context, IMapper mapper, ILogger<ListingsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ListingModel>> AllAsync()
        {
            var items = await _context.Listings
                .ProjectTo<ListingModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return items;
        }

        public async Task<ListingModel?> GetByIdAsync(int id)
        {
            var item = await _context.Listings
                .ProjectTo<ListingModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return item;
        }

        public async Task<ListingModel> CreateAsync(ListingModel model)
        {
            var item = _mapper.Map<Listing>(model);

            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<ListingModel?> UpdateByIdAsync(int id, ListingModel model)
        {
            var item = await _context.Listings
                .FirstOrDefaultAsync(t => t.Id == id);

            if (item is null)
            {
                _logger.LogWarning($"No Order was Found in Database with Id: {id}");
                return null;
            }

            _mapper.Map(model, item);
            item.ModifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return _mapper.Map<ListingModel>(item);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var item = await _context.Listings
                .FirstOrDefaultAsync(t => t.Id == id);

            if (item is null)
            {
                _logger.LogWarning($"No Order was Found in Database with Id: {id}");
                return false;
            }

            item.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

