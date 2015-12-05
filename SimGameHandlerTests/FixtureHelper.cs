using System.Collections.Generic;
using cb.core.domain;
using SimGame.Handler.Entities;

namespace SimGameHandlerTests
{
    public class FixtureHelper
    {
        public static ProductType[] GetHandlerProductTypes()
        {
            return new[]
            {
                new ProductType()
                {
                    Id = 1,
                    TimeToManufacture = 10,
                    Name = "Product 1",
                    ManufacturerType = new ManufacturerType(),
                    RequiredProducts = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 2,
                            Quantity = 1,
                        },
                        new Product
                        {
                            ProductTypeId = 3,
                            Quantity = 2
                        },
                        new Product
                        {
                            ProductTypeId = 4,
                            Quantity = 3
                        }
                    }
                },
                new ProductType
                {
                    Id = 2,
                    ManufacturerType = new ManufacturerType(),
                    TimeToManufacture = 10,
                    Name = "Product 2",
                    RequiredProducts = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 3,
                            Quantity = 1,
                        },
                        new Product
                        {
                            ProductTypeId = 4,
                            Quantity = 2,
                        }
                    }
                },
                new ProductType
                {
                    Id = 3,
                    ManufacturerType = new ManufacturerType(),
                    TimeToManufacture = 10,
                    Name = "Product 3",
                    RequiredProducts = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 4,
                            Quantity = 1,
                        }
                    }
                },
                new ProductType
                {
                    TimeToManufacture = 10,
                    ManufacturerType = new ManufacturerType(),
                    Id = 4,
                    Name = "Product 4",
                    RequiredProducts = new List<Product>()
                }
            };
        }

        public static SimGame.Domain.ProductType[] GetDomainProductTypes()
        {
            return new[]
            {
                new SimGame.Domain.ProductType()
                {
                    Id = 1,
                    TimeToManufacture = 10,
                    Name = "Product 1",
                    ManufacturerType = new SimGame.Domain.ManufacturerType(),
                    RequiredProducts = new List<SimGame.Domain.Product>
                    {
                        new SimGame.Domain.Product
                        {
                            ProductTypeId = 2,
                            Quantity = 1,
                        },
                        new SimGame.Domain.Product
                        {
                            ProductTypeId = 3,
                            Quantity = 2
                        },
                        new SimGame.Domain.Product
                        {
                            ProductTypeId = 4,
                            Quantity = 3
                        }
                    }
                },
                new SimGame.Domain.ProductType
                {
                    Id = 2,
                    ManufacturerType = new SimGame.Domain.ManufacturerType(),
                    TimeToManufacture = 10,
                    Name = "Product 2",
                    RequiredProducts = new List<SimGame.Domain.Product>
                    {
                        new SimGame.Domain.Product
                        {
                            ProductTypeId = 3,
                            Quantity = 1,
                        },
                        new SimGame.Domain.Product
                        {
                            ProductTypeId = 4,
                            Quantity = 2,
                        }
                    }
                },
                new SimGame.Domain.ProductType
                {
                    Id = 3,
                    ManufacturerType = new SimGame.Domain.ManufacturerType(),
                    TimeToManufacture = 10,
                    Name = "Product 3",
                    RequiredProducts = new List<SimGame.Domain.Product>
                    {
                        new SimGame.Domain.Product
                        {
                            ProductTypeId = 4,
                            Quantity = 1,
                        }
                    }
                },
                new SimGame.Domain.ProductType
                {
                    TimeToManufacture = 10,
                    ManufacturerType = new SimGame.Domain.ManufacturerType(),
                    Id = 4,
                    Name = "Product 4",
                    RequiredProducts = new List<SimGame.Domain.Product>()
                }
            };
        }

        public static CityStorage GetCityStorage()
        {
            return new CityStorage
            {
                CurrentInventory = new[]
                {
                    new Product
                    {
                        Id = 1,
                        ProductTypeId = 1
                    },
                    new Product
                    {
                        Id = 2,
                        ProductTypeId = 2
                    },
                    new Product
                    {
                        Id = 3,
                        ProductTypeId = 3
                    },
                    new Product
                    {
                        Id = 4,
                        ProductTypeId = 4
                    }
                }
            };
        }
    }
}