using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Calculators
{
    public class BuildingUpgradeStatisticsCalculator : IBuildingUpgradeStatisticsCalculator
    {
        private readonly IBuildingUpgradeDurationCalculator _buildingUpgradeCalculator;

        public BuildingUpgradeStatisticsCalculator(IBuildingUpgradeDurationCalculator buildingUpgradeCalculator)
        {
            _buildingUpgradeCalculator = buildingUpgradeCalculator;
        }

        public BuildingUpgradeStatisticsCalculatorResponse CalculateStatistics(
            BuildingUpgradeStatisticsCalculatorRequest statisticsRequest)
        {
            if (statisticsRequest.BuildingUpgrades == null)
                return new BuildingUpgradeStatisticsCalculatorResponse();
            foreach (var upgrade in statisticsRequest.BuildingUpgrades)
            {
                var durationRequest = new BuildingUpgradeDurationCalculatorRequest
                {
                    BuildingUpgrade = upgrade,
                    ProductTypes = statisticsRequest.ProductTypes,
                    CityStorage = statisticsRequest.CityStorage
                };
                upgrade.RemainingUpgradeTime = _buildingUpgradeCalculator.CalculateRemainingTime(durationRequest).RemainingUpgradeTime;
            }
            //need to go through each building upgrade and
            //  calculate time left. 
            //  update product list with quantity left after storage 

            return new BuildingUpgradeStatisticsCalculatorResponse();
        }
    }
}