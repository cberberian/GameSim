using System;
using System.Collections.Generic;
using cb.core.domain;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class Manufacturer : DomainObject
    {
        public override int Id { get; set; }
        public virtual ManufacturerType ManufacturerType { get; set; }
        public int ManufacturerTypeId { get; set; }
        public int QueueSize { get; set; }
        public virtual ICollection<ManufacturingQueueSlot> ManufacturingQueueSlots { get; set; }
        public string Description { get; set; }
        public bool IsCityStorage { get; set; }
    }
}