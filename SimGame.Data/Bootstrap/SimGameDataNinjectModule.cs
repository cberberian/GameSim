using System.Configuration;
using Ninject.Modules;
using SimGame.Data.Interface;
using SimGame.Data.Mock;
using SimGame.Data.Repository;
using SimGame.Data.UnitOfWork;

namespace SimGame.Data.Bootstrap
{
    public class SimGameDataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            bool mock;
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockGameSimContext"), out mock) && mock)
                Bind<IGameSimContext>().To<MockGameSimContext>();
            else
                Bind<IGameSimContext>().To<GameSimContext>();
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockManufacturerTypeRepository"), out mock) && mock)
                Bind<IManufacturerTypeRepository>().To<MockManufacturerTypeRepository>();
            else
                Bind<IManufacturerTypeRepository>().To<ManufacturerTypeRepository>();
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockManufacturerTypeUnitOfWork"), out mock) && mock)
                Bind<IManufacturerTypeUnitOfWork>().To<MockManufacturerTypeUnitOfWork>();
            else
                Bind<IManufacturerTypeUnitOfWork>().To<ManufacturerTypeUnitOfWork>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockPropertyUpgradeUoW"), out mock) && mock)
                Bind<IPropertyUpgradeUoW>().To<MockPropertyUpgradeUoW>();
            else
                Bind<IPropertyUpgradeUoW>().To<PropertyUpgradeUoW>();
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockProductTypeRepository"), out mock) && mock)
                Bind<IProductTypeRepository>().To<MockProductTypeRepository>();
            else
                Bind<IProductTypeRepository>().To<ProductTypeRepository>();
        }
    }
}