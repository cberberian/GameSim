using AutoMapper;
using SimGame.Handler.Bootstrap;
using HandlerEntities = SimGame.Handler.Entities;
using MvcApplication1.Models; 

namespace MvcApplication1
{
    public class AutoMapperConfig
    {
        public static void ConfigureMappings()
        {
            //Model to Handler
            Mapper.CreateMap<BuildingUpgrade, HandlerEntities.BuildingUpgrade>();
            Mapper.CreateMap<ManufacturerType, HandlerEntities.ManufacturerType>();
            Mapper.CreateMap<Manufacturer, HandlerEntities.ManufacturerType>();
            Mapper.CreateMap<ProductType, HandlerEntities.ProductType>();
            Mapper.CreateMap<Product, HandlerEntities.Product>();
            Mapper.CreateMap<City, HandlerEntities.Legacy.City>();
            Mapper.CreateMap<CityStorage, HandlerEntities.CityStorage>();

            Mapper.CreateMap<HandlerEntities.BuildingUpgrade, BuildingUpgrade>();
            Mapper.CreateMap<HandlerEntities.ManufacturerType, ManufacturerType>();
            Mapper.CreateMap<HandlerEntities.Manufacturer, Manufacturer>();
            Mapper.CreateMap<HandlerEntities.ProductType, ProductType>();
            Mapper.CreateMap<HandlerEntities.Product, Product>();
            Mapper.CreateMap<HandlerEntities.Legacy.City, City>();
            Mapper.CreateMap<HandlerEntities.CityStorage, CityStorage>();
            HandlerAutomapper.Configure();
        }
    }
}