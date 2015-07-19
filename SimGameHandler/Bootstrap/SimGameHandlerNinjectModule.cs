using System.Configuration;
using Ninject.Modules;
using SimGame.Handler.Calculators;
using SimGame.Handler.Handlers;
using SimGame.Handler.Interfaces;
using SimGame.Handler.Mock;

namespace SimGame.Handler.Bootstrap
{
    public class SimGameHandlerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            bool mock;
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockPropertyUpgradeDurationCalculator"), out mock) && mock)
                Bind<IPropertyUpgradeDurationCalculator>().To<MockPropertyUpgradeDurationCalculator>();
            else
                Bind<IPropertyUpgradeDurationCalculator>().To<PropertyUpgradeDurationCalculator>();
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockPropertyUpgradeHandler"), out mock) && mock)
                Bind<IPropertyUpgradeHandler>().To<MockPropertyUpgradeHandler>();
            else
                Bind<IPropertyUpgradeHandler>().To<PropertyUpgradeHandler>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockManufacturerTypeSearchHandler"), out mock) && mock)
                Bind<IManufacturerTypeSearchHandler>().To<MockManufacturerTypeSearchHandler>();
            else
                Bind<IManufacturerTypeSearchHandler>().To<ManufacturerTypeSearchHandler>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockBuildingUpgradProductConsolidator"), out mock) && mock)
                Bind<IBuildingUpgradProductConsolidator>().To<MockBuildingUpgradProductConsolidator>();
            else
                Bind<IBuildingUpgradProductConsolidator>().To<BuildingUpgradProductConsolidator>();
            Bind<IPropertyUpgradeHandler>().To<PropertyUpgradeHandler>();
            Bind<IInventoryFlattener>().To<InventoryFlattener>();


        }
    }
}