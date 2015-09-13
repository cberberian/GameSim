namespace SimGame.WebApi.Models
{
    public class BuildingUpgrade
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int TotalUpgradeTime { get; set; }
        public int RemainingUpgradeTime { get; set; }
        public int Priority { get; set; }

        public string RemainingUpgradeTimeString
        {
            get
            {
                return string.Format("{0:D2}:{1:D2} hrs", (RemainingUpgradeTime / 60), (RemainingUpgradeTime % 60));
            }
        }
        public string TotalUpgradeTimeString
        {
            get
            {
                return string.Format("{0:D2}:{1:D2} hrs", (TotalUpgradeTime / 60), (TotalUpgradeTime % 60));
            }
        }
        public Product[] Products { get; set; }
        public Product[] ProductsInStorage { get; set; }
        public string Tooltip { get; set; }
    }
}