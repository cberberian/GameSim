using System;
using System.Collections.Generic;
using cb.core.domain;

namespace SimGame.Domain
{
    public class Product : DomainObject
    {
        public DateTime? StartManufactureDateTime { get; set; }
        public override int Id { get; set; }
        public int? Quantity { get; set; }
        public int ProductTypeId { get; set; }
        public int? RequiredByTypeId { get; set; }
        public int? ManufacturerId { get; set; }        
        public int? TimeToFulfill { get; set; }
        public int? OrderId { get; set; }
        public bool IsCityStorage { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<ManufacturingQueueSlot> ManufacturingQueues { get; set; }
        public virtual ProductType RequiredBy { get; set; }
        public virtual BuildingUpgrade BuildingUpgrade { get; set; }
        public int? BuildingUpgradeId { get; set; }
    }
}