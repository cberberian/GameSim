using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeToManufacture { get; set; }
        public int ManufacturerTypeId { get; set; }
        public ICollection<Product> Products { get; set; } 
    }
}