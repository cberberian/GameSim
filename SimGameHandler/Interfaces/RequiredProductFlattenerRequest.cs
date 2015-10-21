using SimGame.Handler.Entities;

namespace SimGame.Handler.Interfaces
{
    public class RequiredProductFlattenerRequest
    {
        public Product[] Products { get; set; }
        public ProductType[] ProductTypes { get; set; }
        public CityStorage CityStorage { get; set; }
    }
}