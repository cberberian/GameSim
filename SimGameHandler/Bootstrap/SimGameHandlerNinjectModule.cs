using System.Configuration;
using Ninject.Modules;
using SimGame.Handler.Calculators;
using SimGame.Handler.Entities;
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
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockBuildingUpgradeDurationCalculator"), out mock) && mock)
                Bind<IBuildingUpgradeDurationCalculator>().To<MockBuildingUpgradeDurationCalculator>();
            else
                Bind<IBuildingUpgradeDurationCalculator>().To<BuildingUpgradeDurationCalculator>();
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockBuildingUpgradeHandler"), out mock) && mock)
                Bind<IBuildingUpgradeHandler>().To<MockBuildingUpgradeHandler>();
            else
                Bind<IBuildingUpgradeHandler>().To<BuildingUpgradeHandler>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockManufacturerTypeSearchHandler"), out mock) && mock)
                Bind<IManufacturerTypeSearchHandler>().To<MockManufacturerTypeSearchHandler>();
            else
                Bind<IManufacturerTypeSearchHandler>().To<ManufacturerTypeSearchHandler>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockBuildingUpgradProductConsolidator"), out mock) && mock)
                Bind<IBuildingUpgradProductConsolidator>().To<MockBuildingUpgradProductConsolidator>();
            else
                Bind<IBuildingUpgradProductConsolidator>().To<BuildingUpgradProductConsolidator>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockCityUpdateHandler"), out mock) && mock)
                Bind<ICityUpdateHandler>().To<MockCityUpdateHandler>();
            else
                Bind<ICityUpdateHandler>().To<CityUpdateHandler>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockBuildingUpgradeStatisticsCalculator"), out mock) && mock)
                Bind<IBuildingUpgradeStatisticsCalculator>().To<MockBuildingUpgradeStatisticsCalculator>();
            else
                Bind<IBuildingUpgradeStatisticsCalculator>().To<BuildingUpgradeStatisticsCalculator>();

            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockCityStorageCalculator"), out mock) && mock)
                Bind<ICityStorageCalculator>().To<MockCityStorageCalculator>();
            else
                Bind<ICityStorageCalculator>().To<CityStorageCalculator>();

            Bind<IInventoryFlattener>().To<InventoryFlattener>();


        }
    }

    public class MockCityStorageCalculator : ICityStorageCalculator
    {
        public CalculateStorageResponse CalculateNewStorageAmounts(CalculateStorageRequest calculateStorageRequest)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MockBuildingUpgradeStatisticsCalculator : IBuildingUpgradeStatisticsCalculator
    {
        public BuildingUpgradeStatisticsCalculatorResponse CalculateStatistics(
            BuildingUpgradeStatisticsCalculatorRequest statisticsRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}