using System.Collections.Generic;

namespace SimGame.Domain
{
    public class Manufacturer : DomainObject
    {
        public virtual ManufacturerType ManufacturerType { get; set; }
        public override int Id { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public int? QueueSize { get; set; }
        public virtual ICollection<ManufacturingQueueSlot> ManufacturingQueueSlots { get; set; }
        public string Description { get; set; }
        public bool IsCityStorage { get; set; }

        public void AddManufacturer(List<ManufacturingQueueSlot> manufacturingQueueSlots)
        {
            foreach (var man in manufacturingQueueSlots)
            {
                AddManufacturer(man);
            }
        }

        private void AddManufacturer(ManufacturingQueueSlot manufacturingQueueSlots)
        {
            if (ManufacturingQueueSlots==null)
                ManufacturingQueueSlots = new List<ManufacturingQueueSlot>();
            ManufacturingQueueSlots.Add(manufacturingQueueSlots);
        }

        
    }
}