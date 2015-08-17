using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Moq;
using NUnit.Framework;
using Should;
using SimGame.Data.Entity;
using SimGame.Data.Interface;
using SimGame.Handler.Calculators;
using SimGame.Handler.Entities;
using Product = SimGame.Domain.Product;
using ProductType = SimGame.Domain.ProductType;

namespace SimGameHandlerTests
{
    [TestFixture]
    public class CityStorageCalculatorTests
    {
        /// <summary>
        /// product 4 update adds to supply
        /// </summary>
        [Test]
        public void scenario_1()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var request = new CalculateStorageRequest
            {
                CityStorage = GetCityStorage(),
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 4,
                        Quantity = 1
                    }, 
                }
            };
            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(1);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(0);
        }

        /// <summary>
        /// product 3 update adds to supply
        /// </summary>
        [Test]
        public void scenario_2()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var request = new CalculateStorageRequest
            {
                CityStorage = GetCityStorage(),
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 3,
                        Quantity = 1
                    }, 
                }
            };
            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(1);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(0);
        }


        /// <summary>
        /// product 2 update adds to supply
        /// </summary>
        [Test]
        public void scenario_3()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var request = new CalculateStorageRequest
            {
                CityStorage = GetCityStorage(),
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 2,
                        Quantity = 1
                    }, 
                }
            };
            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(1);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(0);
        }


        /// <summary>
        /// product 1 update adds to supply
        /// </summary>
        [Test]
        public void scenario_4()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var request = new CalculateStorageRequest
            {
                CityStorage = GetCityStorage(),
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 1,
                        Quantity = 1
                    }, 
                }
            };
            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(1);
        }

        /// <summary>
        /// product 1 update when items already exists adds to supply
        /// </summary>
        [Test]
        public void scenario_5()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity = 1;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 1,
                        Quantity = 2
                    }, 
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(3);
        }

        /// <summary>
        /// add product 3 
            /// increments product 3 by 1 
            /// decrements product 4 by 1
        /// </summary>
        [Test]
        public void scenario_6()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 4;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 3,
                        Quantity = 1
                    }, 
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(3);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(5);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(0);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(0);
        }

        /// <summary>
        /// add product 2
            /// increments product 2 by 1 
            /// decrements product 3 by 1
            /// decrements product 4 by 2
        /// </summary>
        [Test]
        public void scenario_7()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 4;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 2,
                        Quantity = 1
                    }, 
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(2);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(3);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(5);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(0);
        }

        /// <summary>
        /// add product 1
            /// increments product 1 by 1 
            /// decrements product 2 by 1
            /// decrements product 3 by 2
            /// decrements product 3 by 3
        /// </summary>
        [Test]
        public void scenario_8()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 4;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 1,
                        Quantity = 1
                    }, 
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(1);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(2);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(3);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(5);
        }

        /// <summary>
        /// Add 2 products 4
        ///     product 4 increments by 2
        /// </summary>
        [Test]
        public void scenario_9()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 4;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 4;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 4,
                        Quantity = 2
                    }, 
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(6);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(4);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(4);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(4);
        }

        /// <summary>
        /// Add 2 products 3
        ///     product 3 increments by 2
        ///     product 4 decrements by 2
        /// </summary>
        [Test]
        public void scenario_10()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 8;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 3,
                        Quantity = 2
                    }, 
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(6);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(10);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(8);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(8);
        }

        /// <summary>
        /// Add 2 products 2
        ///     product 2 increments by 2
        ///     product 3 decrements by 2
        ///     product 4 decrements by 4
        /// </summary>
        [Test]
        public void scenario_11()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 8;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 2,
                        Quantity = 2
                    }, 
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(4);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(6);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(10);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(8);
        }

        /// <summary>
        /// Add 2 products 1
        ///     product 2 increments by 2
        ///     product 3 decrements by 2
        ///     product 4 decrements by 4
        /// </summary>
        [Test]
        public void scenario_12()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 8;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 1,
                        Quantity = 2
                    }, 
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(2);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(4);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(6);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(10);
        }

        /// <summary>
        /// Add 1 products 3
        /// Add 1 products 4
        ///     product 3 increments by 1
        ///     product 4 stays the same
        /// </summary>
        [Test]
        public void scenario_13()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 8;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 4,
                        Quantity = 1
                    },
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 3,
                        Quantity = 1
                    }
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(8);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(9);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(8);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(8);
        }

        /// <summary>
        /// Add 1 products 2
        /// Add 1 products 3
        ///     product 2 increments by 1
        ///     product 3 stays the same
        ///     product 4 decrements 1 from product 3 and 2 from product 2 (total -3)
        /// </summary>
        [Test]
        public void scenario_14()
        {
            var propertyUpgradeUoW = new Mock<IPropertyUpgradeUoW>();
            propertyUpgradeUoW.Setup(x => x.ProductTypeRepository.Get())
                .Returns(GetDomainProductTypes());
            var calculator = new CityStorageCalculator(propertyUpgradeUoW.Object);
            var cityStorage = GetCityStorage();
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity = 8;
            cityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity = 8;
            var request = new CalculateStorageRequest
            {
                CityStorage = cityStorage,
                NewProductQuantities = new[]
                {
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 3,
                        Quantity = 1
                    },
                    new SimGame.Handler.Entities.Product
                    {
                        ProductTypeId = 2,
                        Quantity = 1
                    }
                }
            };

            var ret = calculator.CalculateNewStorageAmounts(request);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(5);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(8);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(9);
            ret.CityStorage.CurrentInventory.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(8);
        }

       
        #region Helpers

        private CityStorage GetCityStorage()
        {
            return new CityStorage
            {
                CurrentInventory = new[]
                {
                    SupplyProduct1(),
                    SupplyProduct2(),
                    SupplyProduct3(),
                    SupplyProduct4(),
                }
            };
        }

        private SimGame.Handler.Entities.Product SupplyProduct4()
        {
            return new SimGame.Handler.Entities.Product
            {
                ProductTypeId = 4,
                Quantity = 0
            };
        }

        private SimGame.Handler.Entities.Product SupplyProduct3()
        {
            return new SimGame.Handler.Entities.Product
            {
                ProductTypeId = 3,
                Quantity = 0
            };
        }

        private SimGame.Handler.Entities.Product SupplyProduct2()
        {
            return new SimGame.Handler.Entities.Product{
                ProductTypeId = 2,
                Quantity = 0
            };
        }

        private SimGame.Handler.Entities.Product SupplyProduct1()
        {
            return new SimGame.Handler.Entities.Product
            {
                ProductTypeId = 1,
                Quantity = 0
            };
        }

        private IQueryable<ProductType> GetDomainProductTypes()
        {
            return new EnumerableQuery<ProductType>(new[]
            {
                new ProductType()
                {
                    Id = 1,
                    Name = "Product 1",
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
                    Id = 4,
                    Name = "Product 4",
                    RequiredProducts = new List<Product>()
                }
            });
        }

        #endregion

    }


}