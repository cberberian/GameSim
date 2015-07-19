using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Mock
{
    public class MockPropertyUpgradeDurationCalculator : IPropertyUpgradeDurationCalculator
    {
        public PropertyUpgradeDurationCalculatorResponse CalculateUpgradeTime(
            PropertyUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest)
        {
            return new PropertyUpgradeDurationCalculatorResponse
            {
                TotalUpgradeTime = 50
            };
        }
    }
}