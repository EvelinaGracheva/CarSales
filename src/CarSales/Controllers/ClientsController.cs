using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarSales.Data.Entities;
using CarSales.Managers;
using CarSales.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;

namespace CarSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsManager _clientsManager;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientsManager clientsManager, ILogger<ClientsController> logger)
        {
            _clientsManager = clientsManager;
            _logger = logger;
        }


        [HttpGet]
        public async Task<List<ClientModel>> GetClientsList()
        {
            var items = await _clientsManager.GetClientsListAsync();

            return items;
        }

        [HttpGet("GetByPersonalNumber")]
        public async Task<ClientModel> GetClientPersonalNumber(string personalNumber)
        {
            var item = await _clientsManager.GetClientPersonalNumberByAsync(personalNumber);

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ClientModel>> CreateClientAsync(ClientModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            var createItem = await _clientsManager.CreateClientAsync(model);

            return createItem;
        }

        [HttpPut]
        public async Task<ActionResult<ClientModel>> UpdateClientAsync(ClientModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            var updateItem = await _clientsManager.UpdateClientAsync(model);

            return updateItem;
        }

        [HttpDelete]
        public async Task<bool> DeleteClientAsync(string personalNumber)
        {
            return await _clientsManager.DeleteClientAsync(personalNumber);
        }
    }
}