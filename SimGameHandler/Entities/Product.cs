using System;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class Product
    {
        public Product()
        {
        }

        public Product(ProductType invType)
        {
            Name = invType.Name;
            ProductTypeId = invType.Id;
            ProductType = invType;
            ManufacturerTypeId = invType.ManufacturerTypeId;
            TotalDuration = 0;
            Quantity = 0;

        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public int? ProductTypeId { get; set; }
        public int? TotalDuration { get; set; }
        public ProductType ProductType { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? TimeToFulfill { get; set; }
        public int? OrderId { get; set; }
    }
}