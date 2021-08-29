using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Interfaces
{
    public interface IClientsService
    {
        Task<ActionResult<ClientModel>> CreateClientAsync(ClientModel model);
        Task<bool> DeleteClientAsync(string personalNumber);
        Task<ClientModel> GetClientByPersonalNumber(string personalNumber);
        Task<List<ClientModel>> GetClientsList();
        Task<ActionResult<ClientModel>> UpdateClientAsync(ClientModel model);
    }
}
