using System;
using cb.core.domain;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class ManufacturerType : DomainObject
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public int QueueSize { get; set; }
        public bool HasFixedQueueSize { get; set; }
        public bool SupportsParallelManufacturing { get; set; }
        public ProductType[] ProductTypes { get; set; }
    }
}