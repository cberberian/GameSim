using System.Collections.Generic;

namespace SimGame.Domain
{
    public class ProductType : DomainObject
    {
        public override int Id { get; set; }
        public int? TimeToManufacture { get; set; }
        public int SalePriceInDollars { get; set; }
        public string Name { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public virtual ManufacturerType ManufacturerType { get; set; }
        public virtual ICollection<Product> RequiredProducts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        
    }
}
