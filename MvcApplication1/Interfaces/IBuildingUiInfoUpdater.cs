using SimGame.WebApi.Models;
using ProductType = SimGame.Domain.ProductType;

namespace SimGame.WebApi.Interfaces
{
    public interface IBuildingUiInfoUpdater
    {
        void Update(ProductType[] prodTypes, City city);
    }
}