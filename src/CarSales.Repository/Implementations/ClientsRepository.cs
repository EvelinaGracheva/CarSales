using System.Collections.Generic;
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
    public class ClientsRepository : IClientsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientsRepository> _logger;

        public ClientsRepository(ApplicationDbContext context, IMapper mapper, ILogger<ClientsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ClientModel>> GetClientsListAsync()
        {
            var items = await _context.Clients
                .ProjectTo<ClientModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            if (items.Count == 0)
            {
                _logger.LogWarning($"No Clients were Found in Database");
            }

            return items;
        }

        public async Task<ClientModel> GetClientByPersonalNumberAsync(string personalNumber)
        {
            var item = await _context.Clients
                .ProjectTo<ClientModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.PersonalNumber == personalNumber);

            if (item is null)
            {
                _logger.LogWarning($"No Client was Found in Database with PersonalNumber: {personalNumber}");
            }

            return item;
        }

        public async Task<ClientModel> CreateClientAsync(ClientModel model)
        {
            var item = _mapper.Map<Client>(model);

            model.Id = 0;

            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<ClientModel> UpdateClientAsync(ClientModel model)
        {
            var item = await _context.Clients
                .ProjectTo<ClientModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.PersonalNumber == model.PersonalNumber);

            if (item is null)
            {
                _logger.LogWarning($"No Client was Found in Database with PersonalNumber: {model.PersonalNumber}");
            }

            item.Address = model.Address;
            item.Email = model.Email;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.MobileNumber = model.MobileNumber;
            item.PersonalNumber = model.PersonalNumber;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteClientAsync(string personalNumber)
        {
            var item = await _context.Clients
                .ProjectTo<ClientModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.PersonalNumber == personalNumber);

            if (item is null)
            {
                _logger.LogWarning($"No Client was Found in Database with PersonalNumber: {personalNumber}");
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
