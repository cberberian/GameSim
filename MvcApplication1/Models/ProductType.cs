using System.Collections.Generic;

namespace SimGame.WebApi.Models
{
    public class ProductType
    {

        private ManufacturerTypeWrapper _manufacturerType;
        public int Id { get; set; }
        public int SalePriceInDollars { get; set; }
        public string Name { get; set; }
        public int TimeToManufacture { get; set; }
        public int ManufacturerTypeId { get; set; }
        public string ManufacturerName { get; set; }
        public string RequiredProductsToolTip { get; set; }

        public ManufacturerTypeWrapper ManufacturerTypeWrapper
        {
            get
            {
                return _manufacturerType ?? (_manufacturerType = new ManufacturerTypeWrapper
                {
                    Id = ManufacturerTypeId
                });
            }
            set
            {
                _manufacturerType = value;
                if (_manufacturerType != null)
                    ManufacturerTypeId = _manufacturerType.Id;
            }
        }
        public ICollection<Product> Products { get; set; }
        public ICollection<Product> RequiredProducts { get; set; } 
    }

    public class ManufacturerTypeWrapper
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }
}