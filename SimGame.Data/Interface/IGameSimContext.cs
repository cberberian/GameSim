using System.Data.Entity;
using SimGame.Domain;

namespace SimGame.Data.Interface
{
    public interface IGameSimContext
    {
        IDbSet<ProductType> ProductTypes { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<Manufacturer> Manufacturers { get; set; }
        IDbSet<ManufacturerType> ManufacturerTypes { get; set; }
        IDbSet<Order> Orders { get; set; }
        void Commit();
    }
}