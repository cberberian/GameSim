namespace SimGame.Website.Models
{
    public class ManufacturerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QueueSize { get; set; }
        public bool HasFixedQueueSize { get; set; }
        public bool SupportsParallelManufacturing { get; set; }
        public ProductType[] ProductTypes { get; set; }
    }
}