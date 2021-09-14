using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;

namespace CarSales.Repository.Interfaces
{
    public interface IClientsRepository
    {
        Task<ClientModel> CreateAsync(ClientModel model);
        Task<bool> DeleteByPersonalNumberAsync(string personalNumber);
        Task<ClientModel?> GetByPersonalNumberAsync(string personalNumber);
        Task<List<ClientModel>> AllAsync();
        Task<ClientModel?> UpdateByPersonalNumberAsync(string personalNumber, ClientModel model);
        Task<bool> IsPersonalNumberExistsAsync(string personalNumber);
    }
}
