using SimGame.Handler.Entities;

namespace SimGame.Handler.Interfaces
{
    public interface IBuildingUpgradeStatisticsCalculator
    {
        BuildingUpgradeStatisticsCalculatorResponse CalculateStatistics(BuildingUpgradeStatisticsCalculatorRequest statisticsRequest);
    }
}