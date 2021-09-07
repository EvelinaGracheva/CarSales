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
        public async Task<List<ClientModel>> All()
        {
            try
            {
                var items = await _clientsService.AllAsync();

                if (items.Count == 0)
                {
                    _logger.LogWarning($"No Clients were Found in Database");
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Enumerable.Empty<ClientModel>().ToList();
            }
        }

        [HttpGet("{personalNumber}")]
        public async Task<ClientModel> GetByPersonalNumber(string personalNumber)
        {
            try
            {
                var item = await _clientsService.GetByPersonalNumberAsync(personalNumber);

                if (item is null)
                {
                    _logger.LogWarning($"No Client was Found in Database with PersonalNumber: {personalNumber}");
                }

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClientModel>> Create(ClientModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            try
            {
                var createItem = await _clientsService.CreateAsync(model);

                return createItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpPut("{personalNumber}")]
        public async Task<ActionResult<ClientModel>> UpdateByPersonalNumber(string personalNumber, ClientModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(String.Join("; ", ModelState.Values.SelectMany(t => t.Errors.Select(e => e.ErrorMessage))));

                return BadRequest(ModelState);
            }

            try
            {
                var updateItem = await _clientsService.UpdateByPersonalNumberAsync(personalNumber, model);

                return updateItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpDelete("{personalNumber}")]
        public async Task<bool> DeleteByPersonalNumber(string personalNumber)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = await _clientsService.DeleteByPersonalNumberAsync(personalNumber);

                if (!isDeleted)
                {
                    _logger.LogWarning($"No Client was Found in Database with PersonalNumber: {personalNumber}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
            }

            return isDeleted;
        }
    }
}