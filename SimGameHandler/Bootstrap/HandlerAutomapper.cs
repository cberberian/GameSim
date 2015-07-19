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
            Mapper.CreateMap<Product, DataDomain.Manufacturer>();

            //To Handler
            Mapper.CreateMap<DataDomain.ManufacturerType, ManufacturerType>();
            Mapper.CreateMap<DataDomain.ProductType, ProductType>();
            Mapper.CreateMap<DataDomain.Manufacturer, Manufacturer>();
            Mapper.CreateMap<DataDomain.Product, Product>();
        }
    }
}