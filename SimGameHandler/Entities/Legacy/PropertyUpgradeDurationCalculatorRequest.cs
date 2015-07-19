namespace SimGame.Handler.Entities.Legacy
{
    public class PropertyUpgradeDurationCalculatorRequest
    {
        public BuildingUpgrade BuildingUpgrade { get; set; }
        public ProductType[] ProductTypes { get; set; }
    }
}