using System;
using System.Collections.Generic;
using SimGame.Domain;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class ProductType : DomainObject
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public int? TimeToManufacture { get; set; }
        public ManufacturerType ManufacturerType { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public ICollection<Product> RequiredProducts { get; set; }

    }
}