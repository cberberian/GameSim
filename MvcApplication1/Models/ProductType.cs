using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public class ProductType
    {

        private ManufacturerTypeWrapper _manufacturerType;
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeToManufacture { get; set; }
        public int ManufacturerTypeId { get; set; }
        public string RequiredProductsToolTip { get; set; }

        public ManufacturerTypeWrapper ManufacturerTypeWrapper
        {
            get
            {
                if (_manufacturerType == null)
                    _manufacturerType = new ManufacturerTypeWrapper
                    {
                        Id = ManufacturerTypeId
                    };
                return _manufacturerType;
            }
            set
            {
                _manufacturerType = value;
                if (_manufacturerType != null)
                    ManufacturerTypeId = _manufacturerType.Id;
            }
        }
        public ICollection<Product> Products { get; set; } 
    }

    public class ManufacturerTypeWrapper
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }
}