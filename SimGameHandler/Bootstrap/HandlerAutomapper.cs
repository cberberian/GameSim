using AutoMapper;
using SimGame.Handler.Entities;
using DataDomain = SimGame.Domain;

namespace SimGame.Handler.Bootstrap
{
    
    public static class HandlerAutomapper
    {
        public static void Configure()
        {
            //To Domain
            Mapper.CreateMap<ManufacturerType, DataDomain.ManufacturerType>();
            Mapper.CreateMap<ProductType, DataDomain.ProductType>();
            Mapper.CreateMap<Manufacturer, DataDomain.Manufacturer>();
            Mapper.CreateMap<Product, DataDomain.Product>();
            Mapper.CreateMap<BuildingUpgrade, DataDomain.BuildingUpgrade>()
                .ForMember(x=>x.Products, opt=> opt.Ignore());

            //To Handler
            Mapper.CreateMap<DataDomain.ManufacturerType, ManufacturerType>();
            Mapper.CreateMap<DataDomain.ProductType, ProductType>();
            Mapper.CreateMap<DataDomain.Manufacturer, Manufacturer>();
            Mapper.CreateMap<DataDomain.BuildingUpgrade, BuildingUpgrade>();
            Mapper.CreateMap<DataDomain.Product, Product>()
                .ForMember(x => x.SalePriceInDollars, opt => opt.MapFrom(y => y.ProductType == null ? 0 : y.ProductType.SalePriceInDollars))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.ProductType == null ? string.Empty : y.ProductType.Name)); 
        }
    }

}