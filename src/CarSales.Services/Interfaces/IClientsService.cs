using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Interfaces
{
    public interface IClientsService
    {
        Task<ActionResult<ClientModel>> CreateAsync(ClientModel model);
        Task<bool> DeleteByPersonalNumberAsync(string personalNumber);
        Task<ClientModel> GetByPersonalNumberAsync(string personalNumber);
        Task<List<ClientModel>> AllAsync();
        Task<ActionResult<ClientModel>> UpdateByPersonalNumberAsync(string personalNumber, ClientModel model);
    }
}
