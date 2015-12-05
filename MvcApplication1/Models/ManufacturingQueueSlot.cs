using System;

namespace SimGame.WebApi.Models
{
    public class ManufacturingQueueSlot
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public int SlotNumber { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime? DateTimeQueued { get; set; }
        public int? DurationItMuniutes { get; set; }
        public int ManufacturerId { get; set; } 
    }
}