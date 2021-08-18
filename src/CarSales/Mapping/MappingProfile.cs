using AutoMapper;

using CarSales.Data.Entities;
using CarSales.Models;

namespace CarSales.Mapping
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