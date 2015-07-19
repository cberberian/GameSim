using System.Collections.Generic;

namespace SimGame.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductTypeId { get; set; }
        public int? RequiredByTypeId { get; set; }
        public int? ManufacturerId { get; set; }        
        public int? TimeToFulfill { get; set; }
        public int? OrderId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Order Order { get; set; }
        public ICollection<ManufacturingQueueSlot> ManufacturingQueues { get; set; }
        public virtual ProductType RequiredBy { get; set; }
    }
}