using System;
using SimGame.Domain;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class Product : DomainObject
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
        public DateTime? StartManufactureDateTime { get; set; }
        public override int Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public int? ProductTypeId { get; set; }
        public int? TotalDuration { get; set; }
        public ProductType ProductType { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? BuildingUpgradeId { get; set; }
        public int? TimeToFulfill { get; set; }
        public int? OrderId { get; set; }
        public bool IsCityStorage { get; set; }
        /// <summary>
        /// Quantity After City Storage is taken into account. 
        /// </summary>
        public int? RemainingQuantity { get; set; }

        public int? RemainingDuration { get; set; }
        public Product[] RequiredProducts { get; set; }
        public string ParentContext { get; set; }
        public int SalePriceInDollars { get; set; }
    }
}