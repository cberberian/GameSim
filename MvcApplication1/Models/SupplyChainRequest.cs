namespace MvcApplication1.Models
{
    public class SupplyChainRequest
    {
        public bool ReturnInventory { get; set; }
        public bool ReturnFacilityAssignment { get; set; }
        public BuildingUpgrade[] BuildingUpgrades { get; set; }
    }
}