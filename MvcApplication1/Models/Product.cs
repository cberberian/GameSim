using System;
using System.Collections.Generic;

namespace SimGame.WebApi.Models
{
    public class Product
    {
        public Product()
        {
            Keywords = new List<string>();
        }

        private ProductTypeWrapper _productTypeWrapper;
        public int? Id { get; set; }
        public string Name { get; set; }
        public int SalePriceInDollars { get; set; }
        public string RequiredProductsToolTip { get; set; }
        public DateTime? StartManufactureDateTime { get; set; }
        public DateTime? EndManufactureTime {
            get
            {
                if (StartManufactureDateTime.HasValue && TotalDuration.HasValue)
                    return StartManufactureDateTime.Value.AddHours(TotalDuration.Value);
                return null;
            }
        }

        public string Status
        {
            get
            {
                if (!EndManufactureTime.HasValue)
                    return "Pending Manufacture";
                if (EndManufactureTime.Value < DateTime.Now)
                    return "Manufacturing";
                if (EndManufactureTime >= DateTime.Now)
                    return "Complete";
                return "Pending Manufacture";
            }
        }
        public int? Quantity { get; set; }
        public int AdjustedQuantity { get; set; }
        public int? ProductTypeId { get; set; }

        public ProductTypeWrapper ProductTypeWrapper
        {
            get
            {
                return _productTypeWrapper ?? (_productTypeWrapper = new ProductTypeWrapper
                {
                    Id = ProductTypeId,
                    Name = Name
                });
            }
            set
            {
                _productTypeWrapper = value;
                if (_productTypeWrapper == null) return;
                Name = _productTypeWrapper.Name;
                ProductTypeId = _productTypeWrapper.Id;
            }
        }
        public int? BuildingUpgradeId { get; set; }
        public int? TotalDuration { get; set; }
        public string TotalDurationString {
            get
            {
                if (TotalDuration.HasValue)
                    return string.Format("{0:D2}:{1:D2} hrs", TotalDuration.Value / 60, TotalDuration.Value % 60);
                return "00:00 hrs";
            }
        }
        public int? RemainingDuration { get; set; }
        public int? ManufacturerTypeId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? TimeToFulfill { get; set; }
        public int? OrderId { get; set; }
        public bool IsCityStorage { get; set; }
        public int? StorageQuantity { get; set; }
        public Product[] RequiredProducts { get; set; }
        public Product[] NextProducts { get; set; }
        public string ManufacturerName { get; set; }
        public List<string> Keywords { get; set; }
    }

    public class ProductTypeWrapper
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}