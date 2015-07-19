using System.Collections.Generic;

namespace SimGame.Domain
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TimeToManufacture { get; set; }
        public virtual ManufacturerType ManufacturerType { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public virtual ICollection<Product> RequiredProducts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
