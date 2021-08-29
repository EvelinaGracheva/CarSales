using AutoMapper;

using CarSales.Common.Models;
using CarSales.Data.Entities;

namespace CarSales.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarModel>();
            CreateMap<CarModel, Car>();

            CreateMap<Client, ClientModel>();
            CreateMap<ClientModel, Client>();
        }
    }
}