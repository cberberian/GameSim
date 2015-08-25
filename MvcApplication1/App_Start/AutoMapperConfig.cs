using System;
using System.Linq;
using System.Text;
using AutoMapper;
using MvcApplication1.Models;
using SimGame.Handler.Bootstrap;
using HandlerEntities = SimGame.Handler.Entities;
using DomainEntities = SimGame.Domain;


namespace MvcApplication1
{
    public class AutoMapperConfig
    {
        public static void ConfigureMappings()
        {
            //Model to Handler
            Mapper.CreateMap<BuildingUpgrade, HandlerEntities.BuildingUpgrade>()
                .ForMember(x=>x.Id, opt=>opt.MapFrom(y=>y.Id ?? 0));
            Mapper.CreateMap<ManufacturerType, HandlerEntities.ManufacturerType>();
            Mapper.CreateMap<Manufacturer, HandlerEntities.ManufacturerType>();
            Mapper.CreateMap<ManufacturingQueueSlot, HandlerEntities.ManufacturingQueueSlot>();
            Mapper.CreateMap<ProductType, HandlerEntities.ProductType>();
            Mapper.CreateMap<Product, HandlerEntities.Product>();
            Mapper.CreateMap<City, HandlerEntities.City>().ConvertUsing<ModelToHandlerCityConverter>();
            Mapper.CreateMap<CityStorage, HandlerEntities.CityStorage>();

            //Handler to Model
            Mapper.CreateMap<HandlerEntities.BuildingUpgrade, BuildingUpgrade>();
            Mapper.CreateMap<HandlerEntities.ManufacturerType, ManufacturerType>();
            Mapper.CreateMap<HandlerEntities.Manufacturer, Manufacturer>();
            Mapper.CreateMap<HandlerEntities.ManufacturingQueueSlot, ManufacturingQueueSlot>();
            Mapper.CreateMap<HandlerEntities.ProductType, ProductType>()
                .ForMember(x=>x.RequiredProductsToolTip, opt=>opt.ResolveUsing<RequiredProductTypeTooltipResolve>());
            Mapper.CreateMap<HandlerEntities.Product, Product>();
            Mapper.CreateMap<HandlerEntities.City, City>()
                .ForMember(x=>x.BuildingUpgrades, opt=>opt.ResolveUsing<ModelCityBuildingUpgradesResolver>());
            Mapper.CreateMap<HandlerEntities.CityStorage, CityStorage>();
            
            //Model to Domain
            Mapper.CreateMap<ProductType, DomainEntities.ProductType>();
            Mapper.CreateMap<Product, DomainEntities.Product>();
            Mapper.CreateMap<ProductTypeWrapper, DomainEntities.Product>();

            //Domain to Model
            Mapper.CreateMap<DomainEntities.Product, Product>()
                .ForMember(x=>x.RequiredProductsToolTip, opt=>opt.ResolveUsing<RequiredProduct1TooltipResolve>())
                .ForMember(x=>x.Name, opt=>opt.MapFrom(y=> y.ProductType==null ? string.Empty : y.ProductType.Name))
                .ForMember(x => x.SalePriceInDollars, opt => opt.MapFrom(y => y.ProductType == null ? 0 : y.ProductType.SalePriceInDollars))
                .ForMember(x=>x.ManufacturerTypeId, opt=>opt.MapFrom(y=> y.ProductType==null ? 0 : y.ProductType.ManufacturerTypeId));
            Mapper.CreateMap<DomainEntities.ProductType, ProductType>();
            Mapper.CreateMap<DomainEntities.ManufacturerType, ManufacturerType>();
            Mapper.CreateMap<DomainEntities.Manufacturer, Manufacturer>();
            Mapper.CreateMap<DomainEntities.ManufacturingQueueSlot, ManufacturingQueueSlot>()
                .ForMember(x => x.ProductName, opt => opt.MapFrom(y => y.Product == null ? "--available--" : y.Product.ProductType.Name));
            Mapper.CreateMap<DomainEntities.BuildingUpgrade, BuildingUpgrade>();
            HandlerAutomapper.Configure();
        }
    }

    public class ModelCityBuildingUpgradesResolver : ValueResolver<HandlerEntities.City, BuildingUpgrade[]>
    {
        protected override BuildingUpgrade[] ResolveCore(HandlerEntities.City source)
        {
            var ret = source.BuildingUpgrades.Select(Mapper.Map<BuildingUpgrade>).ToArray();
            
            foreach (var bu in ret)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("{0}:{1} hrs of {2}:{3} hrs time remaining<br/>", (bu.RemainingUpgradeTime / 60), (bu.RemainingUpgradeTime % 60), (bu.TotalUpgradeTime / 60), (bu.TotalUpgradeTime % 60));
                foreach (var pd in bu.Products)
                {
                    var strgProd = source.CurrentCityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == pd.ProductTypeId);
                    
                    if (strgProd != null)
                    {
                        var style = strgProd.Quantity < pd.Quantity
                           ? "display: block; color: red"
                           : "display: block";
                        sb.AppendFormat("<span style='{0}'>{1} {2} required of {3} in storage</span>", style, pd.Quantity, pd.Name, strgProd.Quantity);    
                        
                    }
                    else
                        sb.AppendFormat("<span style='display: block'>{0} {1} required", pd.Quantity, pd.Name);    
                }
                bu.Tooltip = sb.ToString();
            }
            return ret; 
        }
    }

    public class RequiredProduct1TooltipResolve : ValueResolver<DomainEntities.Product, string>
    {
        protected override string ResolveCore(DomainEntities.Product source)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("<div>");
                var sourceProductType = source.ProductType ?? new DomainEntities.ProductType();
                sb.AppendFormat("<b><u>{0}</u></b><br/>", sourceProductType.Name);
                if (sourceProductType.RequiredProducts == null || sourceProductType.RequiredProducts.Count == 0)
                    sb.Append("No Dependent products");
                else
                    foreach (var x in sourceProductType.RequiredProducts)
                    {
                        sb.AppendFormat("{0} {1}<br/>", x.Quantity, x.ProductType.Name);
                    }
                sb.Append("</div>");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return string.Empty;
        }
    }

    public class RequiredProductTypeTooltipResolve : ValueResolver<HandlerEntities.ProductType, string>
    {
        protected override string ResolveCore(HandlerEntities.ProductType source)
        {
            
            var sb = new StringBuilder();
            sb.Append("<div>");
            sb.AppendFormat("<b><u>{0}</u></b><br/>", source.Name);
            if (source.RequiredProducts == null || source.RequiredProducts.Count == 0)
                sb.Append("No Dependent products");
            else
                foreach (var x in source.RequiredProducts)
                {
                    sb.AppendFormat("{0} {1}<br/>", x.Quantity, x.Name);
                }
            sb.Append("</div>");
            return sb.ToString();
        }
    }

    public class ModelToHandlerCityConverter : TypeConverter<City, HandlerEntities.City>
    {
        protected override HandlerEntities.City ConvertCore(City source)
        {
            var upgrades = source.BuildingUpgrades;
            if (upgrades==null)
                return new HandlerEntities.City
                {
                    CargoShipOrder = GetCargoShipUpgrade(source),
                    CurrentCityStorage = GetCityStorage(source),
                    //Exclude the city storage from list
                    BuildingUpgrades = new HandlerEntities.BuildingUpgrade[0]
                };
            var buildingUpgrades = upgrades
                .Where(x=> !x.Name.Equals("City Storage"))
                .Select(Mapper.Map<HandlerEntities.BuildingUpgrade>)
                .ToArray();
            return new HandlerEntities.City
            {
                CargoShipOrder = GetCargoShipUpgrade(source),
                CurrentCityStorage = GetCityStorage(source),
                //Exclude the city storage from list
                BuildingUpgrades = buildingUpgrades
            };
        }

        private HandlerEntities.BuildingUpgrade GetCargoShipUpgrade(City source)
        {
            var buildingUpgrades = source.BuildingUpgrades;
            if (buildingUpgrades == null)
                return null;
            return Mapper.Map<HandlerEntities.BuildingUpgrade>(
                            buildingUpgrades.FirstOrDefault(x => x.Name == "Cargo Ship"));
        }

        private HandlerEntities.CityStorage GetCityStorage(City source)
        {
            var ret = new  HandlerEntities.CityStorage();
            if (source.CurrentCityStorage != null)
                return Mapper.Map<HandlerEntities.CityStorage>(source.CurrentCityStorage);
            var cityStorageUpgrade = source.BuildingUpgrades.FirstOrDefault(x => x.Name.Equals("City Storage"));
            ret.CurrentInventory =  cityStorageUpgrade != null && cityStorageUpgrade.Products != null ? 
                                    cityStorageUpgrade.Products
                                        .Select(Mapper.Map<HandlerEntities.Product>)
                                        .ToArray() : 
                                    new HandlerEntities.Product[0];
            return ret;
        }
    }
}