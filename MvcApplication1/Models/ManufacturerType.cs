using System.Collections.Generic;

namespace SimGame.WebApi.Models
{
    public class ManufacturerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QueueSize { get; set; }
        public bool HasFixedQueueSize { get; set; }
        public bool SupportsParallelManufacturing { get; set; }
        public virtual ICollection<ProductType> ProductTypes { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }

        
    }
}