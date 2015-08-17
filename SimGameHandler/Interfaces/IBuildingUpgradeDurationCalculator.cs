using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;

namespace SimGame.Handler.Interfaces
{
    public interface IBuildingUpgradeDurationCalculator
    {
        BuildingUpgradeDurationCalculatorResponse CalculateUpgradeTime(BuildingUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest);
        BuildingUpgradeDurationCalculatorResponse CalculateRemainingTime(BuildingUpgradeDurationCalculatorRequest durationRequest);
    }
}