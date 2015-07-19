using System;
using System.Collections.Generic;

namespace SimGame.Domain
{
    public class ManufacturerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QueueSize { get; set; }
        public bool HasFixedQueueSize { get; set; }
        public bool SupportsParallelManufacturing { get; set; }
        public virtual ICollection<ProductType> ProductTypes { get; set; }
        public ICollection<Manufacturer> Manufacturers { get; set; }

        public void AddProductType(ICollection<ProductType> productTypes)
        {
            if (productTypes == null)
                throw new ApplicationException("Null Product Types Collection Passed to AddProductType");
                
            foreach(var pt in productTypes)
            {
                AddProductType(pt);
            }
        }

        public void AddProductType(ProductType productType)
        {
            if (productType == null)
                throw new ApplicationException("Null Product Type Passed to AddProductType");
            if (ProductTypes == null)
                ProductTypes = new List<ProductType>();
            ProductTypes.Add(productType);
        }
    }
}