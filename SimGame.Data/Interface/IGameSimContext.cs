using System.Data.Entity;
using cb.core.interfaces;
using SimGame.Domain;

namespace SimGame.Data.Interface
{
    public interface IGameSimContext : IEntityContext
    {
        IDbSet<ProductType> ProductTypes { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<Manufacturer> Manufacturers { get; set; }
        IDbSet<ManufacturerType> ManufacturerTypes { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<BuildingUpgrade> BuildingUpgrades { get; set; }

    }
}