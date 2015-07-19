using System;

namespace SimGame.Handler.Entities.Legacy
{
    [Serializable]
    public class City
    {
        public CityStorage CurrentCityStorage { get; set; }
        public Manufacturer[] Manufacturers { get; set; }
        public BuildingUpgrade[] BuildingUpgrades { get; set; }
    }
}