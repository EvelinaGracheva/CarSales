using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarSales.Common.Models;
using CarSales.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientsService clientsService, ILogger<ClientsController> logger)
        {
            _clientsService = clientsService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<List<ClientModel>> GetClientsList()
        {
            var items = await _clientsService.GetClientsList();

            return items;
        }

        [HttpGet("GetByPersonalNumber")]
        public async Task<ClientModel> GetClientByPersonalNumber(string personalNumber)
        {
            var item = await _clientsService.GetClientByPersonalNumber(personalNumber);

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ClientModel>> CreateClient(ClientModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            var createItem = await _clientsService.CreateClientAsync(model);

            return createItem;
        }

        [HttpPut]
        public async Task<ActionResult<ClientModel>> UpdateClient(ClientModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            var updateItem = await _clientsService.UpdateClientAsync(model);

            return updateItem;
        }

        [HttpDelete]
        public async Task<bool> DeleteClient(string personalNumber)
        {
            return await _clientsService.DeleteClientAsync(personalNumber);
        }
    }
}