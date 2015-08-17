namespace MvcApplication1.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string RequiredProductsToolTip { get; set; }
        public int? Quantity { get; set; }
        public int AdjustedQuantity { get; set; }
        public int? ProductTypeId { get; set; }
        public int? BuildingUpgradeId { get; set; }
        public int? TotalDuration { get; set; }
        public string TotalDurationString {
            get
            {
                if (TotalDuration.HasValue)
                    return string.Format("{0:D2}:{1:D2} hrs", TotalDuration.Value / 60, TotalDuration.Value % 60);
                return "00:00 hrs";
            }
        }
        public int? RemainingDuration { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? TimeToFulfill { get; set; }
        public int? OrderId { get; set; }
        public bool IsCityStorage { get; set; }
        public int? StorageQuantity { get; set; }
        public Product[] RequiredProducts { get; set; }
        public Product[] NextProducts { get; set; }
    }
}