using System.Configuration;
using Ninject.Modules;
using SimGame.WebApi.Interfaces;
using SimGame.WebApi.Models;
using ProductType = SimGame.Domain.ProductType;

namespace SimGame.WebApi.Helpers
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
        public void Update(ProductType[] prodTypes, City city)
        {
            throw new System.NotImplementedException();
        }
    }
}