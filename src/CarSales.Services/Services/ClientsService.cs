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
        private readonly IClientsRepository _clientsService;

        public ClientsService(IClientsRepository clientsRepository)
        {
            _clientsService = clientsRepository;
        }

        public async Task<List<ClientModel>> GetClientsList()
        {
            var items = await _clientsService.GetClientsListAsync();

            return items;
        }

        public async Task<ClientModel> GetClientByPersonalNumber(string personalNumber)
        {
            var item = await _clientsService.GetClientByPersonalNumberAsync(personalNumber);

            return item;
        }

        public async Task<ActionResult<ClientModel>> CreateClientAsync(ClientModel model)
        {
            var createItem = await _clientsService.CreateClientAsync(model);

            return createItem;
        }

        public async Task<ActionResult<ClientModel>> UpdateClientAsync(ClientModel model)
        {
            var updateItem = await _clientsService.UpdateClientAsync(model);

            return updateItem;
        }

        public async Task<bool> DeleteClientAsync(string personalNumber)
        {
            return await _clientsService.DeleteClientAsync(personalNumber);
        }
    }
}
