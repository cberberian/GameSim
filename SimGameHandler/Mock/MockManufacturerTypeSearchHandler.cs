using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Mock
{
    public class MockManufacturerTypeSearchHandler : IManufacturerTypeSearchHandler
    {
        public ManufacturerTypeSearchHandlerResponse Get(ManufacturerTypeSearchHandlerRequest request)
        {
            return new ManufacturerTypeSearchHandlerResponse
            {
                Results = new[]
                {
                    new ManufacturerType
                    {
                        HasFixedQueueSize = true,
                        Id = 1,
                        Name = "Factory",
                        QueueSize = 3,
                        SupportsParallelManufacturing = true,
                        ProductTypes = new[]
                        {
                            new ProductType
                            {
                                Id = 1,
                                ManufacturerTypeId = 1,
                                Name = "Metal",
                                TimeToManufacture = 1
                            }
                        }
                    }
                }
            };
        }
    }
}