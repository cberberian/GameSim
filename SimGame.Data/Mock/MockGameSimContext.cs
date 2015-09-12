using System.Data.Entity;
using System.Linq;
using cb.core.interfaces;
using Moq;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Mock
{
    public class MockGameSimContext : IGameSimContext
    {
        

        public IDbSet<ProductType> ProductTypes
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Product> Products
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public IDbSet<Manufacturer> Manufacturers
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public IDbSet<ManufacturerType> ManufacturerTypes
        {
            get { 
                return new MockDbSet<ManufacturerType>(new[]
                {
                    new ManufacturerType
                    {
                        Id = 1,
                        Name = "Factory",
                        HasFixedQueueSize = true,
                        QueueSize = 3,
                        SupportsParallelManufacturing = true,
                        ProductTypes = new[]
                        {
                            new ProductType
                            {
                                ManufacturerTypeId = 1,
                                Id = 1,
                                Name = "Metal",
                                TimeToManufacture = 1
                            },
                            new ProductType
                            {
                                ManufacturerTypeId = 1,
                                Id = 2,
                                Name = "Wood",
                                TimeToManufacture = 1
                            },
                            new ProductType
                            {
                                ManufacturerTypeId = 1,
                                Id = 3,
                                Name = "Plastic",
                                TimeToManufacture = 9
                            },
                        }
                    }  
                }); 
            }
            set {  }
        }

        public IDbSet<Order> Orders
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public IDbSet<BuildingUpgrade> BuildingUpgrades { get; set; }

        public IEntityContext Context { get; set; }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public void SetValues<T>(T domainUpgrade, T changedUpgrade) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T domainObject) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}