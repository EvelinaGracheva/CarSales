﻿using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Data.Entities;
using CarSales.Managers;
using CarSales.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsManager _clientsManager;
        private readonly ILogger _logger;

        public ClientsController(IClientsManager clientsManager, ILogger logger)
        {
            _clientsManager = clientsManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Client>> GetClientsList()
        {
            var items = await _clientsManager.GetClientsListAsync();

            return items;
        }

        [HttpGet("GetByPersonalNumber")]
        public async Task<Client> GetClientPersonalNumber(string personalNumber)
        {
            var item = await _clientsManager.GetClientPersonalNumberByAsync(personalNumber);

            return item;
        }

        [HttpPost]
        public async Task<Client> CreateClientAsync(Client client)
        {
            var createItem = await _clientsManager.CreateClientAsync(client);

            return createItem;
        }

        [HttpPut]
        public async Task<Client> UpdateClientAsync(Client client)
        {
            var updateItem = await _clientsManager.UpdateClientAsync(client);

            return updateItem;
        }

        [HttpDelete]
        public async Task<bool> DeleteClientAsync(string personalNumber)
        {
            return await _clientsManager.DeleteClientAsync(personalNumber);
        }
    }
}