using System;

namespace SimGame.Domain
{
    public class ManufacturingQueueSlot
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public int SlotNumber { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime? DateTimeQueued { get; set; }
        public int? DurationItMuniutes { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
    }
}