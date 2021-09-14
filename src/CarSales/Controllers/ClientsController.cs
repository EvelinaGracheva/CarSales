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
        public async Task<ActionResult<List<ClientModel>>> All()
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{personalNumber}")]
        public async Task<ActionResult<ClientModel>> GetByPersonalNumber(string personalNumber)
        {
            try
            {
                var item = await _clientsService.GetByPersonalNumberAsync(personalNumber);

                if (item is null)
                {
                    _logger.LogWarning($"No Client was Found in Database with PersonalNumber: {personalNumber}");

                    return NotFound();
                }

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
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

                if (updateItem is null)
                {
                    _logger.LogWarning($"No Client was Found in Database with PersonalNumber: {personalNumber}");

                    return NotFound();
                }

                return updateItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{personalNumber}")]
        public async Task<ActionResult<bool>> DeleteByPersonalNumber(string personalNumber)
        {
            try
            {
                bool isDeleted = await _clientsService.DeleteByPersonalNumberAsync(personalNumber);

                if (!isDeleted)
                {
                    _logger.LogWarning($"No Client was Found in Database with PersonalNumber: {personalNumber}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

            return true;
        }
    }
}