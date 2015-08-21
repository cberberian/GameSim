using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public int ManufacturerTypeId { get; set; }
        public int QueueSize { get; set; }
        public virtual ICollection<ManufacturingQueueSlot> ManufacturingQueueSlots { get; set; }
        public string Description { get; set; }
        public bool IsCityStorage { get; set; } 
    }
}