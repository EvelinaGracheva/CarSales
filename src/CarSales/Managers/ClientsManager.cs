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
    public class ClientsManager : IClientsManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientsManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClientModel>> GetClientsListAsync()
        {
            var items = await _context.Clients
                .ProjectTo<ClientModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();

            return items;
        }

        public async Task<ClientModel> GetClientPersonalNumberByAsync(string personalNumber)
        {
            var item = await _context.Clients
                .ProjectTo<ClientModel>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.PersonalNumber == personalNumber);

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
                return null;
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
                return false;
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }

    public interface IClientsManager
    {
        Task<ClientModel> CreateClientAsync(ClientModel model);
        Task<bool> DeleteClientAsync(string personalNumber);
        Task<ClientModel> GetClientPersonalNumberByAsync(string personalNumber);
        Task<List<ClientModel>> GetClientsListAsync();
        Task<ClientModel> UpdateClientAsync(ClientModel model);
    }
}
