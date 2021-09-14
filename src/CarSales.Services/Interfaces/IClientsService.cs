using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Interfaces
{
    public interface IClientsService
    {
        Task<ClientModel> CreateAsync(ClientModel model);
        Task<bool> DeleteByPersonalNumberAsync(string personalNumber);
        Task<ClientModel?> GetByPersonalNumberAsync(string personalNumber);
        Task<List<ClientModel>> AllAsync();
        Task<ClientModel?> UpdateByPersonalNumberAsync(string personalNumber, ClientModel model);
    }
}
