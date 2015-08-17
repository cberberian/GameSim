using System;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Mock
{
    public class MockBuildingUpgradeHandler : IBuildingUpgradeHandler
    {
        public BuildingUpgradeHandlerResponse CalculateBuildQueue(BuildingUpgradeHandlerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}