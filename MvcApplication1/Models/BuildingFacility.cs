namespace MvcApplication1.Models
{
    public class BuildingFacility
    {
        public int FacilityType { get; set; }
        public bool ParallelProcessing { get; set; }
        public int QueueSize { get; set; }
        public InventoryItem[] ManufacturingQueue { get; set; }
    }
}