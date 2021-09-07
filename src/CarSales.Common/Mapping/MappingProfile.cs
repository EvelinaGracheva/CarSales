using AutoMapper;

using CarSales.Common.Models;
using CarSales.Data.Entities;

namespace CarSales.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientModel>();
            CreateMap<ClientModel, Client>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Listings, opt => opt.Ignore())
                .ForMember(dest => dest.Purchases, opt => opt.Ignore());

            CreateMap<Listing, ListingModel>();
            CreateMap<ListingModel, Listing>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Client, opt => opt.Ignore())
                .ForMember(dest => dest.Purchase, opt => opt.Ignore())
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore());

            CreateMap<Purchase, PurchaseModel>();
            CreateMap<PurchaseModel, Purchase>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Client, opt => opt.Ignore());

            CreateMap<Vehicle, VehicleModel>();
            CreateMap<VehicleModel, Vehicle>()
                .ForMember(dest => dest.Listings, opt => opt.Ignore());
        }
    }
}