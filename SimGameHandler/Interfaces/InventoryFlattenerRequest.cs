using SimGame.Handler.Entities;

namespace SimGame.Handler.Interfaces
{
    public class InventoryFlattenerRequest
    {
        public BuildingUpgrade[] BuildingUpgrades { get; set; }
        public ProductType[] ProductTypes { get; set; }
    }
}