using System.Configuration;
using MvcApplication1.Interfaces;
using MvcApplication1.Models;
using Ninject.Modules;
using SimGame.Handler.Calculators;
using SimGame.Handler.Interfaces;
using SimGame.Handler.Mock;
using ProductType = SimGame.Domain.ProductType;

namespace MvcApplication1.Helpers
{
    public class WebApplication1NinjectModule : NinjectModule
    {
        public override void Load()
        {
            bool mock;
//            IBuildingUiInfoUpdater
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("MockBuildingUiInfoUpdater"), out mock) && mock)
                Bind<IBuildingUiInfoUpdater>().To<MockBuildingUiInfoUpdater>();
            else
                Bind<IBuildingUiInfoUpdater>().To<BuildingUiInfoUpdater>();
        }
    }

    public class MockBuildingUiInfoUpdater : IBuildingUiInfoUpdater
    {
        public void Update(BuildingUpgrade[] buildingUpgrades, CityStorage currentCityStorage, ProductType[] prodTypes)
        {
            throw new System.NotImplementedException();
        }
    }
}