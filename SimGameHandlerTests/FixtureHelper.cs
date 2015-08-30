using System.Collections.Generic;
using SimGame.Handler.Entities;

namespace SimGameHandlerTests
{
    public class FixtureHelper
    {
        public static ProductType[] GetRepositoryProductTypes()
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
    }
}