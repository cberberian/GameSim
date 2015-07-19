using System;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class BuildingUpgradeProductConsoldatorRequest
    {
        public BuildingUpgrade[] BuildingUpgrades { get; set; }

        public CityStorage CityStorage { get; set; }

        public ProductType[] ProductTypes { get; set; }
    }
}