using SimGame.Handler.Entities.Legacy;

namespace SimGame.Handler.Interfaces
{
    public interface IBuildingUpgradeHandler
    {
        BuildingUpgradeHandlerResponse CalculateBuildQueue(BuildingUpgradeHandlerRequest request);
    }
}