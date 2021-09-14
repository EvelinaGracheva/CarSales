using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CarSales.Common.Models;
using CarSales.Repository.Interfaces;
using CarSales.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CarSales.Services.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IVehiclesRepository _vehiclesRepository;

        public VehiclesService(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }

        public async Task<List<VehicleModel>> AllAsync()
        {
            var items = await _vehiclesRepository.AllAsync();

            return items;
        }

        public async Task<VehicleModel?> GetByVinCodeAsync(string vinCode)
        {
            var item = await _vehiclesRepository.GetByVinCodeAsync(vinCode);

            return item;
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel model)
        {
            if (await _vehiclesRepository.IsVinCodeExistsAsync(model.VinCode))
            {
                throw new Exception($"VinCode: {model.VinCode} is already exists");
            }

            var createItem = await _vehiclesRepository.CreateAsync(model);

            return createItem;
        }

        public async Task<VehicleModel?> UpdateByVinCodeAsync(string vinCode, VehicleModel model)
        {
            if (vinCode != model.VinCode && await _vehiclesRepository.IsVinCodeExistsAsync(model.VinCode))
            {
                throw new Exception($"VinCode: {model.VinCode} is already exists");
            }

            var updateItem = await _vehiclesRepository.UpdateByVinCodeAsync(vinCode, model);

            return updateItem;
        }

        public async Task<bool> DeleteByVinCodeAsync(string vinCode)
        {
            return await _vehiclesRepository.DeleteByVinCodeAsync(vinCode);
        }
    }
}
