namespace MvcApplication1.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public int? ProductTypeId { get; set; }
        public int? TotalDuration { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? TimeToFulfill { get; set; }
        public int? OrderId { get; set; }
    }
}