
namespace SimGame.WebApi.Models
{
    public class City
    {
        public City()
        {
            NewManufacturingQueueSlots = new ManufacturingQueueSlot[0];
        }

        public Manufacturer[] Manufacturers { get; set; }
        public BuildingUpgrade[] BuildingUpgrades { get; set; }
        public CityStorage CurrentCityStorage { get; set; }
        public Product[] RequiredProducts { get; set; }
        public Product[] AvailableStorage { get; set; }
        public Product[] PendingInventory { get; set; }
        public Product[] TotalProductsRequired { get; set; }
        public Product[] RequiredProductsInCityStorage { get; set; }
        public Product[] TotalProducts { get; set; }
        public ManufacturingQueueSlot[] NewManufacturingQueueSlots { get; set; }
    }
}