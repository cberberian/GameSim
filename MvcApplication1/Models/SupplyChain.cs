namespace SimGame.WebApi.Models
{
    public class SupplyChain
    {
        public Product[] RequiredProducts { get; set; }
        public CityStorage CurrentCityStorage { get; set; }
        public Product[] TotalProducts { get; set; }
        public Product[] AvailableStorage { get; set; }
        public Product[] RequiredProductsInCityStorage { get; set; }
        public BuildingUpgrade[] BuildingUpgrades { get; set; }
        public City City { get; set; }
    }
}