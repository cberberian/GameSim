using SimGame.WebApi.Models;
using ProductType = SimGame.Domain.ProductType;

namespace MvcApplication1.Interfaces
{
    public interface IBuildingUiInfoUpdater
    {
        void Update(BuildingUpgrade[] buildingUpgrades, CityStorage currentCityStorage, ProductType[] prodTypes);
    }
}