
namespace MvcApplication2.Models
{
    public class City
    {
        public Manufacturer[] Manufacturers { get; set; }
        public BuildingUpgrade[] BuildingUpgrades { get; set; }
        public CityStorage CurrentCityStorage { get; set; }
        public ManufacturerType[] ManufacturerTypes { get; set; }
    }
}