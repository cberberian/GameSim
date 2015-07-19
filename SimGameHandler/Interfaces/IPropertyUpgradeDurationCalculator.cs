using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;

namespace SimGame.Handler.Interfaces
{
    public interface IPropertyUpgradeDurationCalculator
    {
        PropertyUpgradeDurationCalculatorResponse CalculateUpgradeTime(PropertyUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest);
    }
}