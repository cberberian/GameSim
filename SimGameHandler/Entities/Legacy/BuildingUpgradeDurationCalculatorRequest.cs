namespace SimGame.Handler.Entities.Legacy
{
    public class BuildingUpgradeDurationCalculatorRequest
    {
        public BuildingUpgrade BuildingUpgrade { get; set; }
        public ProductType[] ProductTypes { get; set; }
        public CityStorage CityStorage { get; set; }
    }
}