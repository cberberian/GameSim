using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;
using SimGame.Handler.Calculators;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;

namespace SimGameHandlerTests
{
    [TestFixture]
    public class InventoryFlattenerTests
    {

        /// <summary>
        /// 1 product with 1 product type returns 1 
        /// </summary>
        [Test]
        public void scenario_1()
        {
            var flattener = new InventoryFlattener();
            
            var buildingUpgrades = new[]
            {
                new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 1,
                            Quantity = 1
                        }
                    }
                }
            };

            ProductType[] productTypes = {
                new ProductType
                {
                    Id = 1,
                    Name = "name"
                }    
            };
            var inventoryFlattenerRequest = new InventoryFlattenerRequest
            {
                Products = buildingUpgrades.SelectMany(x=>x.Products).ToArray(),
                ProductTypes = productTypes
            };
            var ret = flattener.GetFlattenedInventory(inventoryFlattenerRequest);
            ret.Products.Count().ShouldEqual(1);
        }

        /// <summary>
        /// 1 product with 1 product type 1 depenent product returns 2
        /// </summary>
        [Test]
        public void scenario_2()
        {
            var flattener = new InventoryFlattener();

            var buildingUpgrades = new[]
            {
                new BuildingUpgrade
                {
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 1,
                            Quantity = 1
                        }
                    }
                }
            };

            ProductType[] productTypes = {
                new ProductType
                {
                    Id = 1,
                    Name = "name 1",
                    RequiredProducts = new List<Product>
                    {
                        new Product
                        {
                            ProductTypeId = 2,
                            Quantity = 2
                        }
                    }
                },
                new ProductType
                {
                    Id = 2,
                    Name = "name 2"
                } 
            };
            var inventoryFlattenerRequest = new InventoryFlattenerRequest
            {
                Products = buildingUpgrades.SelectMany(x=>x.Products).ToArray(),
                ProductTypes = productTypes
            };
            var ret = flattener.GetFlattenedInventory(inventoryFlattenerRequest);
            ret.Products.Count().ShouldEqual(2);
            ret.Products.Any(x=>x.ProductTypeId==1).ShouldBeTrue();
            ret.Products.Any(x=>x.ProductTypeId==2).ShouldBeTrue();
        }

        [Test]
        public void findDupes()
        {
            var ar1 = new int[]
            {
                1, 2, 3, 3, 4
            };
            var hsh1 = new HashSet<int>(ar1);
            Assert.True(hsh1.Count < ar1.Length);
        }

    }
}