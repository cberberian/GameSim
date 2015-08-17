using SimGame.Handler.Entities;

namespace SimGame.Handler.Interfaces
{
    public class InventoryFlattenerRequest
    {
        public Product[] Products { get; set; }
        public ProductType[] ProductTypes { get; set; }
    }
}