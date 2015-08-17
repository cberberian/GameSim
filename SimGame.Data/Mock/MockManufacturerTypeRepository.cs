using System.Collections.ObjectModel;
using System.Linq;
using SimGame.Data.Entity;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Mock
{
    public class MockManufacturerTypeRepository : IManufacturerTypeRepository
    {
        public IGameSimContext Context { get; set; }

        public IQueryable<ManufacturerType> Get()
        {
            return new EnumerableQuery<ManufacturerType>(new ManufacturerType[]
            {
                new ManufacturerType
                {
                    Id = 1,
                    Name = "Basic Factory",
                    HasFixedQueueSize = true,
                    SupportsParallelManufacturing = true, 
                    QueueSize = 3,
                    ProductTypes = new Collection<ProductType>
                    {
                        new ProductType
                        {
                            Id = 1,
                            ManufacturerTypeId = 1,
                            Name = "Metal",
                            TimeToManufacture = 1
                        },
                        new ProductType
                        {
                            Id = 2,
                            ManufacturerTypeId = 1,
                            Name = "Wood",
                            TimeToManufacture = 3
                        }
                    }
                } 
            });
        }

        public IQueryable<ManufacturerType> Get(RepositoryRequest<ManufacturerType> request)
        {
            return new EnumerableQuery<ManufacturerType>(new[]
            {
                new ManufacturerType
                {
                    Id = 1,
                    Name = "Basic Factory",
                    HasFixedQueueSize = true,
                    SupportsParallelManufacturing = true, 
                    QueueSize = 3,
                    ProductTypes = new Collection<ProductType>
                    {
                        new ProductType
                        {
                            Id = 1,
                            ManufacturerTypeId = 1,
                            Name = "Metal",
                            TimeToManufacture = 1
                        },
                        new ProductType
                        {
                            Id = 2,
                            ManufacturerTypeId = 1,
                            Name = "Wood",
                            TimeToManufacture = 3
                        }
                    }
                } 
            });
        }

        public void Add(ManufacturerType entity)
        {
            throw new System.NotImplementedException();
        }

        public void SetValues(ManufacturerType dest, ManufacturerType chng)
        {
            throw new System.NotImplementedException();
        }
    }
}