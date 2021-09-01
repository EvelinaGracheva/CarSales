using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;
using CarSales.Repository.Interfaces;
using CarSales.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _clientsRepository;

        public ClientsService(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public async Task<List<ClientModel>> GetClientsListAsync()
        {
            var items = await _clientsRepository.GetClientsListAsync();

            return items;
        }

        public async Task<ClientModel> GetClientByPersonalNumberAsync(string personalNumber)
        {
            var item = await _clientsRepository.GetClientByPersonalNumberAsync(personalNumber);

            return item;
        }

        public async Task<ActionResult<ClientModel>> CreateClientAsync(ClientModel model)
        {
            var createItem = await _clientsRepository.CreateClientAsync(model);

            return createItem;
        }

        public async Task<ActionResult<ClientModel>> UpdateClientAsync(ClientModel model)
        {
            var updateItem = await _clientsRepository.UpdateClientAsync(model);

            return updateItem;
        }

        public async Task<bool> DeleteClientAsync(string personalNumber)
        {
            return await _clientsRepository.DeleteClientAsync(personalNumber);
        }
    }
}
