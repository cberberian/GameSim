namespace SimGame.WebApi.Models
{
    public class BuildingFacilityType
    {
        public virtual string Name { get; set; }
        public virtual int QueueSize { get; set; }
        public virtual int FacilityType { get; set; }
        public virtual bool ParallelProcessing { get; set; }
        public virtual InventoryItem[] SupportedItems { get; set; }
    }
}