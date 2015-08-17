using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Should;
using SimGame.Data.Interface;
using SimGame.Handler.Calculators;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;

namespace SimGameHandlerTests
{
    [TestFixture]
    public class BuildingUpgradeDurationCalculatorTests
    {
        #region Set1 
        /// <summary>
        /// when 1 product id 4 
        ///     none in storage.
        /// then
        ///     takes 10 minutes
        ///     remaining 10 minutes
        /// </summary>
        [Test]
        public void Scenario1()
        {
            var uow = new Mock<IPropertyUpgradeUoW>();
            var calculator = new BuildingUpgradeDurationCalculator(uow.Object);
            var request = new BuildingUpgradeDurationCalculatorRequest
            {
                BuildingUpgrade = new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 4,
                            Quantity = 1
                        }
                    }
                },
                CityStorage = CityStorage(),
                ProductTypes = ProductTypes()
            };
            var ret = calculator.CalculateUpgradeTime(request);
            Assert.IsNotNull(ret);
            request.BuildingUpgrade.TotalUpgradeTime.ShouldEqual(10);
            request.BuildingUpgrade.RemainingUpgradeTime.ShouldEqual(10);
            request.BuildingUpgrade.Products.First().TotalDuration.ShouldEqual(10);
            request.BuildingUpgrade.Products.First().RemainingDuration.ShouldEqual(10);
        }

        /// <summary>
        /// when 2 product id 4 
        ///     none in storage.
        /// then
        ///     bu TotalTime takes 20 minutes
        ///     bu RemainingTime 20 minutes
        ///     prod 4 TotalTime takes 20 minutes
        ///     prod 4 RemainingTime 20 minutes
        /// </summary>
        [Test]
        public void Scenario1A()
        {
            var uow = new Mock<IPropertyUpgradeUoW>();
            var calculator = new BuildingUpgradeDurationCalculator(uow.Object);
            var request = new BuildingUpgradeDurationCalculatorRequest
            {
                BuildingUpgrade = new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 4,
                            Quantity = 2
                        }
                    }
                },
                CityStorage = CityStorage(),
                ProductTypes = ProductTypes()
            };
            var ret = calculator.CalculateUpgradeTime(request);
            Assert.IsNotNull(ret);
            request.BuildingUpgrade.TotalUpgradeTime.ShouldEqual(20);
            request.BuildingUpgrade.RemainingUpgradeTime.ShouldEqual(20);
            request.BuildingUpgrade.Products.First().TotalDuration.ShouldEqual(20);
            request.BuildingUpgrade.Products.First().RemainingDuration.ShouldEqual(20);
        }

        #endregion


        /// <summary>
        /// when 1 product id 3 
        ///     none in storage.
        /// then
        ///     bu TotalTime takes 20 minutes
        ///     bu RemainingTime 20 minutes
        ///     prod 3 TotalTime takes 20 minutes
        ///     prod 3 RemainingTime 20 minutes
        ///     child prod 4 TotalTime takes 10 minutes
        ///     child prod 4 RemainingTime 10 minutes
        /// </summary>
        [Test]
        public void Scenario2()
        {
            var uow = new Mock<IPropertyUpgradeUoW>();
            var calculator = new BuildingUpgradeDurationCalculator(uow.Object);
            var request = new BuildingUpgradeDurationCalculatorRequest
            {
                BuildingUpgrade = new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 3,
                            Quantity = 1
                        }
                    }
                },
                CityStorage = CityStorage(),
                ProductTypes = ProductTypes()
            };
            var ret = calculator.CalculateUpgradeTime(request);
            Assert.IsNotNull(ret);
            request.BuildingUpgrade.TotalUpgradeTime.ShouldEqual(20);
            request.BuildingUpgrade.RemainingUpgradeTime.ShouldEqual(20);
            request.BuildingUpgrade.Products.First().TotalDuration.ShouldEqual(20);
            request.BuildingUpgrade.Products.First().RemainingDuration.ShouldEqual(20);

            request.BuildingUpgrade.Products.First().RequiredProducts.FirstOrDefault().ShouldNotBeNull();
            request.BuildingUpgrade.Products.First().RequiredProducts.Count().ShouldEqual(1);
            request.BuildingUpgrade.Products.First().RequiredProducts.First().TotalDuration.ShouldEqual(10);
            request.BuildingUpgrade.Products.First().RequiredProducts.First().RemainingDuration.ShouldEqual(10);
        }

        /// <summary>
        /// when 2 product id 3 
        ///     none in storage.
        /// then
        ///     bu TotalTime takes 40 minutes
        ///     bu RemainingTime 40 minutes
        ///     prod 3 TotalTime takes 40 minutes
        ///     prod 3 RemainingTime 40 minutes
        ///     child prod 4 TotalTime takes 20 minutes
        ///     child prod 4 RemainingTime 20 minutes
        /// </summary>
        [Test]
        public void Scenario2A()
        {
            var uow = new Mock<IPropertyUpgradeUoW>();
            var calculator = new BuildingUpgradeDurationCalculator(uow.Object);
            var request = new BuildingUpgradeDurationCalculatorRequest
            {
                BuildingUpgrade = new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 3,
                            Quantity = 2
                        }
                    }
                },
                CityStorage = CityStorage(),
                ProductTypes = ProductTypes()
            };
            var ret = calculator.CalculateUpgradeTime(request);
            Assert.IsNotNull(ret);
            request.BuildingUpgrade.TotalUpgradeTime.ShouldEqual(40);
            request.BuildingUpgrade.RemainingUpgradeTime.ShouldEqual(40);
            request.BuildingUpgrade.Products.First().TotalDuration.ShouldEqual(40);
            request.BuildingUpgrade.Products.First().RemainingDuration.ShouldEqual(40);

            request.BuildingUpgrade.Products.First().RequiredProducts.FirstOrDefault().ShouldNotBeNull();
            request.BuildingUpgrade.Products.First().RequiredProducts.Count().ShouldEqual(1);
            request.BuildingUpgrade.Products.First().RequiredProducts.First().TotalDuration.ShouldEqual(20);
            request.BuildingUpgrade.Products.First().RequiredProducts.First().RemainingDuration.ShouldEqual(20);
        }

        /// <summary>
        /// when 1 product id 2
        ///     none in storage.
        /// then
        ///     bu TotalTime takes 50 minutes
        ///     bu RemainingTime takes 50 minutes
        ///     prod 2 TotalTime 50
        ///     prod 2 RemainingTime 50
        ///     prod 3 TotalTime 20
        ///     prod 3 RemainingTime 20
        ///     prod 4 TotalTime 20
        ///     prod 4 RemainingTime 20
        /// </summary>
        [Test]
        public void Scenario3()
        {
            var uow = new Mock<IPropertyUpgradeUoW>();
            var calculator = new BuildingUpgradeDurationCalculator(uow.Object);
            var request = new BuildingUpgradeDurationCalculatorRequest
            {
                BuildingUpgrade = new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 2,
                            Quantity = 1
                        }
                    }
                },
                CityStorage = CityStorage(),
                ProductTypes = ProductTypes()
            };
            var ret = calculator.CalculateUpgradeTime(request);
            Assert.IsNotNull(ret);
            ret.TotalUpgradeTime.ShouldEqual(50);
            ret.RemainingUpgradeTime.ShouldEqual(50);

            request.BuildingUpgrade.Products.First().TotalDuration.ShouldEqual(50);
            request.BuildingUpgrade.Products.First().RemainingDuration.ShouldEqual(50);

            request.BuildingUpgrade.Products.First().RequiredProducts.FirstOrDefault().ShouldNotBeNull();
            request.BuildingUpgrade.Products.First().RequiredProducts.Count().ShouldEqual(2);
            request.BuildingUpgrade.Products.First().RequiredProducts.ToArray()[0].TotalDuration.ShouldEqual(20);
            request.BuildingUpgrade.Products.First().RequiredProducts.ToArray()[0].RemainingDuration.ShouldEqual(20);

            request.BuildingUpgrade.Products.First().RequiredProducts.ToArray()[1].TotalDuration.ShouldEqual(20);
            request.BuildingUpgrade.Products.First().RequiredProducts.ToArray()[1].RemainingDuration.ShouldEqual(20);

        }

        /// <summary>
        /// 1 product id 4 
        ///     takes 10 minutes
        ///     remaining 0 minutes (1 already in storage)
        /// </summary>
        [Test]
        public void Scenario4()
        {
            var uow = new Mock<IPropertyUpgradeUoW>();
            var calculator = new BuildingUpgradeDurationCalculator(uow.Object);
            var cityStorage = CityStorage();
            var prod = cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4);
            prod.Quantity = 1;
            var request = new BuildingUpgradeDurationCalculatorRequest
            {
                BuildingUpgrade = new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 4,
                            Quantity = 1
                        }
                    }
                },
                CityStorage = cityStorage,
                ProductTypes = ProductTypes()
            };
            var ret = calculator.CalculateUpgradeTime(request);
            Assert.IsNotNull(ret);
            ret.TotalUpgradeTime.ShouldEqual(10);
            ret.RemainingUpgradeTime.ShouldEqual(0);

        }


        /// <summary>
        /// 1 product id 3 
        ///     takes 20 minutes (product 4 + product 3)
        ///     10 minutes remaining (1 product 4 in storage)
        /// </summary>
        [Test]
        public void Scenario5()
        {
            var uow = new Mock<IPropertyUpgradeUoW>();
            var calculator = new BuildingUpgradeDurationCalculator(uow.Object);
            var cityStorage = CityStorage();
            var prod = cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4);
            prod.Quantity = 1;
            var request = new BuildingUpgradeDurationCalculatorRequest
            {
                BuildingUpgrade = new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 3,
                            Quantity = 1
                        }
                    }
                },
                CityStorage = cityStorage,
                ProductTypes = ProductTypes()
            };
            var ret = calculator.CalculateUpgradeTime(request);
            Assert.IsNotNull(ret);
            ret.TotalUpgradeTime.ShouldEqual(20);
            ret.RemainingUpgradeTime.ShouldEqual(10);
        }


        private static CityStorage CityStorage()
        {
            return new CityStorage
            {
                CurrentInventory = new[]
                {
                    new Product{ ProductTypeId = 1, Quantity = 0}, 
                    new Product{ ProductTypeId = 2, Quantity = 0 }, 
                    new Product{ ProductTypeId = 3, Quantity = 0 }, 
                    new Product{ ProductTypeId = 4, Quantity = 0 }
                }
            };
        }

        private static ProductType[] ProductTypes()
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

//
//        [Test]
//        public void null_propertyupgrade_returns_non_null_result()
//        {
//            var calculator = new buildingUpgradeDurationCalculator();
//            var request = new BuildingUpgradeDurationCalculatorRequest();
//            var ret = calculator.CalculateUpgradeTime(request);
//            Assert.IsNotNull(ret);
//        }
//
//        [Test]
//        public void zero_metal_returns_total_time_0()
//        {
//            var calculator = new buildingUpgradeDurationCalculator();
//            var request = new BuildingUpgradeDurationCalculatorRequest
//            {
//                BuildingUpgrade = new BuildingUpgrade
//                {
//                    RequiredInventoryItems = new List<LegacyProduct>
//                    {
//                        new Metal()
//                    }
//                }
//            };
//            var ret = calculator.CalculateUpgradeTime(request);
//            Assert.AreEqual(0, ret.TotalUpgradeTime);
//        }
//
//        [Test]
//        public void one_metal_returns_total_time_1()
//        {
//            var calculator = new buildingUpgradeDurationCalculator();
//            var request = new BuildingUpgradeDurationCalculatorRequest
//            {
//                BuildingUpgrade = new BuildingUpgrade
//                {
//                    RequiredInventoryItems = new List<LegacyProduct>
//                    {
//                        new Metal
//                        {
//                            Quantity = 1
//                        }
//                    }
//                }
//            };
//            var ret = calculator.CalculateUpgradeTime(request);
//            Assert.AreEqual(1, ret.TotalUpgradeTime);
//        }
//
//        [Test]
//        public void one_nail_returns_total_time_6()
//        {
//            var calculator = new buildingUpgradeDurationCalculator();
//            var request = new BuildingUpgradeDurationCalculatorRequest
//            {
//                BuildingUpgrade = new BuildingUpgrade
//                {
//                    RequiredInventoryItems = new List<LegacyProduct>
//                    {
//                        new Nail
//                        {
//                            Quantity = 1
//                        }
//                    }
//                }
//            };
//            var ret = calculator.CalculateUpgradeTime(request);
//            Assert.AreEqual(6, ret.TotalUpgradeTime);
//        }
//
//        [Test]
//        public void two_nails_returns_total_time_11() //parallel time for pre requisite metal
//        {
//            var calculator = new buildingUpgradeDurationCalculator();
//            var request = new BuildingUpgradeDurationCalculatorRequest
//            {
//                BuildingUpgrade = new BuildingUpgrade
//                {
//                    RequiredInventoryItems = new List<LegacyProduct>
//                    {
//                        new Nail
//                        {
//                            Quantity = 2
//                        }
//                    }
//                }
//            };
//            var ret = calculator.CalculateUpgradeTime(request);
//            Assert.AreEqual(11, ret.TotalUpgradeTime);
//        }
//
//        [Test]
//        public void factory_inventoryItem_calculates_using_parallel()
//        {
//            var calculator = new buildingUpgradeDurationCalculator();
//            var request = new BuildingUpgradeDurationCalculatorRequest
//            {
//                BuildingUpgrade = new BuildingUpgrade
//                {
//                    RequiredInventoryItems = new List<LegacyProduct>
//                    {
//                        new Metal //
//                        {
//                            Quantity = 2
//                        }
//                    }
//                }
//            };
//            var ret = calculator.CalculateUpgradeTime(request);
//            Assert.AreEqual(1, ret.TotalUpgradeTime); //Should only have duration for 1 
//        }
//
////        [Test]
////        public void null_requiredInventoryItems_returns_non_null_result()
////        {
////            var calculator = new buildingUpgradeDurationCalculator();
////            var request = new BuildingUpgradeDurationCalculatorRequest
////            {
////                BuildingUpgrade = new BuildingUpgrade()
////                {
////                    RequiredInventoryItems = new List<LegacyProduct>
////                    {
////                        new Metal()
////                    }
////                }
////            };
////            var ret = calculator.CalculateUpgradeTime(request);
////            Assert.IsNotNull(ret);
////        }

    }
}