using System;
using System.Collections.Generic;

namespace SimGame.Domain
{
    public class ManufacturerType : DomainObject
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public int? QueueSize { get; set; }
        public bool HasFixedQueueSize { get; set; }
        public bool SupportsParallelManufacturing { get; set; }
        public virtual ICollection<ProductType> ProductTypes { get; set; }
        public ICollection<Manufacturer> Manufacturers { get; set; }

    }
}