using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Mock
{
    public class MockBuildingUpgradeDurationCalculator : IBuildingUpgradeDurationCalculator
    {
        public BuildingUpgradeDurationCalculatorResponse CalculateUpgradeTime(
            BuildingUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest)
        {
            return new BuildingUpgradeDurationCalculatorResponse
            {
                TotalUpgradeTime = 50
            };
        }

        public BuildingUpgradeDurationCalculatorResponse CalculateRemainingTime(
            BuildingUpgradeDurationCalculatorRequest durationRequest)
        {
            throw new System.NotImplementedException();
        }

        public BuildingUpgradeDurationCalculatorResponse CalculateUpgradeTimes(
            BuildingUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}