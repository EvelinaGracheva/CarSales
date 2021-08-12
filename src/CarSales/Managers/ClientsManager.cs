using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarSales.Data;
using CarSales.Models;

using Microsoft.EntityFrameworkCore;

namespace CarSales.Managers
{
    public class ClientsManager: IClientsManager
    {
        private readonly ApplicationDbContext _context;

        public ClientsManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetClientsListAsync()
        {
            var items = await _context.Clients.AsNoTracking().ToListAsync();

            return items;
        }

        public async Task<Client> GetClientPersonalNumberByAsync(string personalNumber)
        {
            var item = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(t => t.PersonalNumber == personalNumber);

            return item;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            client.Id = 0;
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            var item = await _context.Clients.FirstOrDefaultAsync(t => t.PersonalNumber == client.PersonalNumber);

            if (item is null)
            {
                return null;
            }

            item.Address = client.Address;
            item.Email = client.Email;
            item.FirstName = client.FirstName;
            item.LastName = client.LastName;
            item.MobileNumber = client.MobileNumber;
            item.PersonalNumber = client.PersonalNumber;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteClientAsync(string personalNumber)
        {
            var item = await _context.Clients.FirstOrDefaultAsync(t => t.PersonalNumber == personalNumber);

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
        Task<Client> CreateClientAsync(Client client);
        Task<bool> DeleteClientAsync(string personalNumber);
        Task<Client> GetClientPersonalNumberByAsync(string personalNumber);
        Task<List<Client>> GetClientsListAsync();
        Task<Client> UpdateClientAsync(Client client);
    }
}
