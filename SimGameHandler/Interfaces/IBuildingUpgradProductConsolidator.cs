using SimGame.Handler.Bootstrap;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;

namespace SimGame.Handler.Interfaces
{
    public interface IBuildingUpgradProductConsolidator
    {
        BuildingUpgradeProductConsolidatorResponse GetConsolidatedProductQueue(BuildingUpgradeProductConsoldatorRequest request);
    }
}