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

        public async Task<List<ClientModel>> AllAsync()
        {
            var items = await _clientsRepository.AllAsync();

            return items;
        }

        public async Task<ClientModel> GetByPersonalNumberAsync(string personalNumber)
        {
            var item = await _clientsRepository.GetByPersonalNumberAsync(personalNumber);

            return item;
        }

        public async Task<ActionResult<ClientModel>> CreateAsync(ClientModel model)
        {
            var createItem = await _clientsRepository.CreateAsync(model);

            return createItem;
        }

        public async Task<ActionResult<ClientModel>> UpdateByPersonalNumberAsync(string personalNumber, ClientModel model)
        {
            var updateItem = await _clientsRepository.UpdateByPersonalNumberAsync(personalNumber, model);

            return updateItem;
        }

        public async Task<bool> DeleteByPersonalNumberAsync(string personalNumber)
        {
            return await _clientsRepository.DeleteByPersonalNumberAsync(personalNumber);
        }
    }
}
