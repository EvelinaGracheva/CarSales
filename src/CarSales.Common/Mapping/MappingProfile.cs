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
            CreateMap<CarModel, Car>()
                .ForMember(d => d.Orders, opt => opt.Ignore());

            CreateMap<Client, ClientModel>();
            CreateMap<ClientModel, Client>()
                .ForMember(d => d.Orders, opt => opt.Ignore());

            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>()
                .ForMember(d => d.Car, opt => opt.Ignore())
                .ForMember(d => d.Client, opt => opt.Ignore());
        }
    }
}