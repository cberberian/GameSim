using NUnit.Framework;

namespace SimGameHandlerTests
{
    [TestFixture]
    public class BuildingUpgradeHandlerTests
    {

//        [Test]
//        public void null_request_return_valid_inventory_queue()
//        {
//            var buildingUpgradeDurationCalculator = new Mock<IBuildingUpgradeDurationCalculator>();
//            var handler = new BuildingUpgradeHandler(buildingUpgradeDurationCalculator.Object);
//            var response = handler.CalculateBuildQueue(null);
//            Assert.IsNotNull(response.RequiredProductQueue);
//        }
//
//        [Test]
//        public void null_city_return_valid_inventory_queue()
//        {
//            var buildingUpgradeDurationCalculator = new Mock<IBuildingUpgradeDurationCalculator>();
//            var handler = new BuildingUpgradeHandler(buildingUpgradeDurationCalculator.Object);
//            var request = new BuildingUpgradeHandlerRequest();
//            var response = handler.CalculateBuildQueue(request);
//            Assert.IsNotNull(response.RequiredProductQueue);
//        }
//
//        [Test]
//        public void null_city_facilities_return_valid_inventory_queue()
//        {
//            var buildingUpgradeDurationCalculator = new Mock<IBuildingUpgradeDurationCalculator>();
//            var handler = new BuildingUpgradeHandler(buildingUpgradeDurationCalculator.Object);
//            var request = new BuildingUpgradeHandlerRequest
//            {
//                City = new City()
//            };
//            var response = handler.CalculateBuildQueue(request);
//            Assert.IsNotNull(response.RequiredProductQueue);
//        }
//
//        [Test]
//        public void unordered_updgrades_returns_ordered_result()
//        {
//            var buildingUpgradeDurationCalculator = new Mock<IBuildingUpgradeDurationCalculator>();
//            var queueOfResponses = new Queue<BuildingUpgradeDurationCalculatorResponse>();
//            queueOfResponses.Enqueue(new BuildingUpgradeDurationCalculatorResponse
//            {
//                TotalUpgradeTime = 10
//            });
//            queueOfResponses.Enqueue(new BuildingUpgradeDurationCalculatorResponse
//            {
//                TotalUpgradeTime = 8
//            });
//            queueOfResponses.Enqueue(new BuildingUpgradeDurationCalculatorResponse
//            {
//                TotalUpgradeTime = 9
//            });
//
//            buildingUpgradeDurationCalculator.Setup(
//                x => x.CalculateUpgradeTime(It.IsAny<BuildingUpgradeDurationCalculatorRequest>()))
//                .Returns(queueOfResponses.Dequeue);
//            
//            var handler = new BuildingUpgradeHandler(buildingUpgradeDurationCalculator.Object);
//            var city = new City
//            {
//                CurrentBuildingFacilities = new BuildingFacility[] {}
//            };
//            city.AddPropertyUpgrade(new BuildingUpgrade());
//            city.AddPropertyUpgrade(new BuildingUpgrade());
//            city.AddPropertyUpgrade(new BuildingUpgrade());
//
//            var request = new BuildingUpgradeHandlerRequest
//            {
//                City = city
//            };
//
//            var response = handler.CalculateBuildQueue(request);
//            var propertyUpgrades = response.Products.ToArray();
//            Assert.AreEqual(8, propertyUpgrades[0].TotalUpgradeTime);
//            Assert.AreEqual(9, propertyUpgrades[1].TotalUpgradeTime);
//            Assert.AreEqual(10, propertyUpgrades[2].TotalUpgradeTime);
//        }
//
//        [Test]
//        public void requested_upgrade_is_added_to_manufacture_queue()
//        {
//            var buildingUpgradeDurationCalculator = new Mock<IBuildingUpgradeDurationCalculator>();
//            var queueOfResponses = new Queue<BuildingUpgradeDurationCalculatorResponse>();
//            queueOfResponses.Enqueue(new BuildingUpgradeDurationCalculatorResponse
//            {
//                TotalUpgradeTime = 10
//            });
//
//            buildingUpgradeDurationCalculator.Setup(
//                x => x.CalculateUpgradeTime(It.IsAny<BuildingUpgradeDurationCalculatorRequest>()))
//                .Returns(queueOfResponses.Dequeue);
//            
//            var handler = new BuildingUpgradeHandler(buildingUpgradeDurationCalculator.Object);
//            var city = new City
//            {
//                CurrentBuildingFacilities = new BuildingFacility[]
//                {
//                    new BasicFactory()
//                }
//            };
//            city.AddPropertyUpgrade(new BuildingUpgrade
//            {
//               RequiredInventoryItems = { new Metal
//               {
//                   Quantity = 2
//               } }
//            });
//
//            var request = new BuildingUpgradeHandlerRequest
//            {
//                City = city
//            };
//
//            var response = handler.CalculateBuildQueue(request);
//
//            var manufacturingQueue = response.ManufactureOrder.BuildingFacilities.First().ManufacturingQueue;
//            Assert.AreEqual(2, manufacturingQueue.Count);
//            var inventoryItem = manufacturingQueue.Peek();
//
//            Assert.IsTrue(inventoryItem is Metal); 
//
//        }
//
//        [Test]
//        public void requested_upgrade_is_wrong_type_added_to_pending_queue()
//        {
//            var buildingUpgradeDurationCalculator = new Mock<IBuildingUpgradeDurationCalculator>();
//            var queueOfResponses = new Queue<BuildingUpgradeDurationCalculatorResponse>();
//            queueOfResponses.Enqueue(new BuildingUpgradeDurationCalculatorResponse
//            {
//                TotalUpgradeTime = 10
//            });
//
//            buildingUpgradeDurationCalculator.Setup(
//                x => x.CalculateUpgradeTime(It.IsAny<BuildingUpgradeDurationCalculatorRequest>()))
//                .Returns(queueOfResponses.Dequeue);
//            
//            var handler = new BuildingUpgradeHandler(buildingUpgradeDurationCalculator.Object);
//            var city = new City
//            {
//                CurrentBuildingFacilities = new BuildingFacility[]
//                {
//                    new BasicFactory()
//                }
//            };
//
//            city.AddPropertyUpgrade(new BuildingUpgrade
//            {
//               RequiredInventoryItems = { new Vegetable
//               {
//                   Quantity = 1
//               } }
//            });
//
//            var request = new BuildingUpgradeHandlerRequest
//            {
//                City = city
//            };
//
//            var response = handler.CalculateBuildQueue(request);
//
//            var inventoryItem = response.PendingInventory.FirstOrDefault();
//
//            Assert.IsTrue(inventoryItem is Vegetable); 
//
//        }
//
//        [Test]
//        public void requested_upgrade_has_more_than_max_only_max_amount_added()
//        {
//            var buildingUpgradeDurationCalculator = new Mock<IBuildingUpgradeDurationCalculator>();
//            var queueOfResponses = new Queue<BuildingUpgradeDurationCalculatorResponse>();
//            queueOfResponses.Enqueue(new BuildingUpgradeDurationCalculatorResponse
//            {
//                TotalUpgradeTime = 10
//            });
//
//            buildingUpgradeDurationCalculator.Setup(
//                x => x.CalculateUpgradeTime(It.IsAny<BuildingUpgradeDurationCalculatorRequest>()))
//                .Returns(queueOfResponses.Dequeue);
//
//            var handler = new BuildingUpgradeHandler(buildingUpgradeDurationCalculator.Object);
//            var city = new City
//            {
//                CurrentBuildingFacilities = new BuildingFacility[]
//                {
//                    new BasicFactory()
//                }
//            };
//
//            city.AddPropertyUpgrade(new BuildingUpgrade
//            {
//                RequiredInventoryItems = { new Metal
//                {
//                   Quantity = 4
//               } }
//            });
//
//            var request = new BuildingUpgradeHandlerRequest
//            {
//                City = city
//            };
//
//            var response = handler.CalculateBuildQueue(request);
//
//            //Main assert only max amount added. 
//            Assert.AreEqual(3, response.ManufactureOrder.BuildingFacilities.First().ManufacturingQueue.Count);
//            //assert that we still have pending inventory
//            Assert.AreEqual(1, response.PendingInventory.Length);
//            //assert that the count remaining is only 1
//            Assert.AreEqual(1, response.PendingInventory.First().Quantity);
//
//
//        }
//
//        [Test]
//        public void requested_upgrade_has_more_than_max_pending_has_only_balance_remaining()
//        {
//            var buildingUpgradeDurationCalculator = new Mock<IBuildingUpgradeDurationCalculator>();
//            var queueOfResponses = new Queue<BuildingUpgradeDurationCalculatorResponse>();
//            queueOfResponses.Enqueue(new BuildingUpgradeDurationCalculatorResponse
//            {
//                TotalUpgradeTime = 10
//            });
//
//            buildingUpgradeDurationCalculator.Setup(
//                x => x.CalculateUpgradeTime(It.IsAny<BuildingUpgradeDurationCalculatorRequest>()))
//                .Returns(queueOfResponses.Dequeue);
//
//            var handler = new BuildingUpgradeHandler(buildingUpgradeDurationCalculator.Object);
//            var city = new City
//            {
//                CurrentBuildingFacilities = new BuildingFacility[]
//                {
//                    new BasicFactory()
//                }
//            };
//
//            city.AddPropertyUpgrade(new BuildingUpgrade
//            {
//                RequiredInventoryItems = { new Metal
//                {
//                   Quantity = 4
//               } }
//            });
//
//            var request = new BuildingUpgradeHandlerRequest
//            {
//                City = city
//            };
//
//            var response = handler.CalculateBuildQueue(request);
//
//            //assert that we still have pending inventory
//            Assert.AreEqual(1, response.PendingInventory.Length);
//            //assert that the count remaining is only 1
//            Assert.AreEqual(1, response.PendingInventory.First().Quantity);
//
//
//        }


    }
}
