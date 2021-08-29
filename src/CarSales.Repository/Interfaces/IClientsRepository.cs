using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

namespace CarSales.Repository.Interfaces
{
    public interface IClientsRepository
    {
        Task<ClientModel> CreateClientAsync(ClientModel model);
        Task<bool> DeleteClientAsync(string personalNumber);
        Task<ClientModel> GetClientByPersonalNumberAsync(string personalNumber);
        Task<List<ClientModel>> GetClientsListAsync();
        Task<ClientModel> UpdateClientAsync(ClientModel model);
    }
}
