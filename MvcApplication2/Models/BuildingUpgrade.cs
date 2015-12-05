namespace SimGame.Website.Models
{
    public class BuildingUpgrade
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int TotalUpgradeTime { get; set; }
        public int RemainingUpgradeTime { get; set; }
        public Product[] Products { get; set; }
        public Product[] ProductsInStorage { get; set; }
    }
}