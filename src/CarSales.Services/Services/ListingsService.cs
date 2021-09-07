using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;
using CarSales.Repository.Interfaces;
using CarSales.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Services
{
    public class ListingsService : IListingsService
    {
        private readonly IListingsRepository _listingsRepository;

        public ListingsService(IListingsRepository listingsRepository)
        {
            _listingsRepository = listingsRepository;
        }

        public async Task<List<ListingModel>> AllAsync()
        {
            var items = await _listingsRepository.AllAsync();

            return items;
        }

        public async Task<ListingModel> GetByIdAsync(int id)
        {
            var item = await _listingsRepository.GetByIdAsync(id);

            return item;
        }

        public async Task<ActionResult<ListingModel>> CreateAsync(ListingModel model)
        {
            var createItem = await _listingsRepository.CreateAsync(model);

            return createItem;
        }

        public async Task<ActionResult<ListingModel>> UpdateByIdAsync(int id, ListingModel model)
        {
            var updateItem = await _listingsRepository.UpdateByIdAsync(id, model);

            return updateItem;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _listingsRepository.DeleteByIdAsync(id);
        }
    }
}
