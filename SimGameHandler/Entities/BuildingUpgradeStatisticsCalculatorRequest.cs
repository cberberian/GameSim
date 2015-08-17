using System.Collections.Generic;

namespace SimGame.Handler.Entities
{
    public class BuildingUpgradeStatisticsCalculatorRequest
    {
        public IEnumerable<BuildingUpgrade> BuildingUpgrades { get; set; }
        public ProductType[] ProductTypes { get; set; }
        public CityStorage CityStorage { get; set; }
    }
}