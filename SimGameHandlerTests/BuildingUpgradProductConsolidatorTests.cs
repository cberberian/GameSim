using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Should;
using SimGame.Data.Interface;
using SimGame.Domain;
using SimGame.Handler.Calculators;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;
using BuildingUpgrade = SimGame.Handler.Entities.BuildingUpgrade;
using ManufacturerType = SimGame.Handler.Entities.ManufacturerType;
using Product = SimGame.Handler.Entities.Product;
using ProductType = SimGame.Handler.Entities.ProductType;

namespace SimGameHandlerTests
{
    [TestFixture]
    public class BuildingUpgradProductConsolidatorTests
    {

        [Test]
        public void null_request_returns_empty_response()
        {
            var uow = new Mock<IPropertyUpgradeUoW>();
            var invFlattener = new Mock<IInventoryFlattener>();
            var consolidator = new BuildingUpgradProductConsolidator(uow.Object, invFlattener.Object);
            var ret = consolidator.GetConsolidatedProductQueue(null);

            ret.ShouldNotBeNull();
            ret.ConsolidatedRequiredProductQueue.ShouldNotBeNull();
            ret.ConsolidatedRequiredProductQueue.Count.ShouldEqual(0);

        }

        /// <summary>
        /// when 
        ///     1 product
        ///     1 minute time to manufacture
        ///     parallel true
        /// then 
        ///     quantity should be 1
        ///     total duration should be 1
        /// </summary>
        [Test]
        public void scenario_1()
        {
            //1 product quantity 1 returned from flattener
            var flattenerResult = new InventoryFlattenerResponse
            {
                Products = new[]
                {
                    new Product
                    {
                        ProductTypeId = 1,
                        Quantity = 1,
                    }, 
                }
            };
            //product type matched time to manufacture 1
            var productTypes = new[]
            {
                new ProductType
                {
                    Id = 1,
                    Name = "test",
                    RequiredProducts = new List<Product>(),
                    TimeToManufacture = 1,
                    ManufacturerType = new ManufacturerType
                    {
                        SupportsParallelManufacturing = true
                    }
                        
                }
            };

            var uow = new Mock<IPropertyUpgradeUoW>();
            var invFlattener = new Mock<IInventoryFlattener>();
            invFlattener
                .Setup(x => x.GetFlattenedInventory(It.IsAny<InventoryFlattenerRequest>()))
                .Returns(flattenerResult);
            
            
            
            var consolidator = new BuildingUpgradProductConsolidator(uow.Object, invFlattener.Object);
            
            var request = new BuildingUpgradeProductConsoldatorRequest
            {
                BuildingUpgrades = new BuildingUpgrade[0],
                ProductTypes = productTypes,
                
            };
            var ret = consolidator.GetConsolidatedProductQueue(request);

            ret.ConsolidatedRequiredProductQueue.Count.ShouldEqual(1);
            ret.ConsolidatedRequiredProductQueue.First().Quantity.ShouldEqual(1);
            ret.ConsolidatedRequiredProductQueue.First().TotalDuration.ShouldEqual(1);

        }


        /// <summary>
        /// when 
        ///     2 products same product type 
        ///         1 product quantity 1
        ///         1 product quantity 2
        ///     time to manufacture: 1
        ///     manufacture parallel: true
        /// then 
        ///     quantity should be 3
        ///     total duration should be 1
        /// </summary>
        [Test]
        public void scenario_2()
        {
            //1 product quantity 1 returned from flattener
            var flattenerResult = new InventoryFlattenerResponse
            {
                Products = new[]
                {
                    new Product
                    {
                        ProductTypeId = 1,
                        Quantity = 1,
                    }, 
                    new Product
                    {
                        ProductTypeId = 1,
                        Quantity = 2
                    }
                }
            };
            //product type matched time to manufacture 1
            var productTypes = new[]
            {
                new ProductType
                {
                    Id = 1,
                    Name = "test",
                    RequiredProducts = new List<Product>(),
                    TimeToManufacture = 1,
                    ManufacturerType = new ManufacturerType
                    {
                        SupportsParallelManufacturing = true
                    }
                        
                }
            };

            var uow = new Mock<IPropertyUpgradeUoW>();
            var invFlattener = new Mock<IInventoryFlattener>();
            invFlattener
                .Setup(x => x.GetFlattenedInventory(It.IsAny<InventoryFlattenerRequest>()))
                .Returns(flattenerResult);



            var consolidator = new BuildingUpgradProductConsolidator(uow.Object, invFlattener.Object);

            var request = new BuildingUpgradeProductConsoldatorRequest
            {
                BuildingUpgrades = new BuildingUpgrade[0],
                ProductTypes = productTypes,

            };
            var ret = consolidator.GetConsolidatedProductQueue(request);

            ret.ConsolidatedRequiredProductQueue.Count.ShouldEqual(1);
            ret.ConsolidatedRequiredProductQueue.First().Quantity.ShouldEqual(3);
            ret.ConsolidatedRequiredProductQueue.First().TotalDuration.ShouldEqual(1);

        }

        /// <summary>
        /// when 
        ///     1 product quantity 2
        ///     1 required product quanity 1
        ///     time to manufacture: 1
        ///     manufacture parallel: true
        /// then 
        ///     quantity should be 1
        ///     required 
        ///     total duration should be 1
        /// </summary>
        [Test]
        public void scenario_3()
        {

        }


    }
}