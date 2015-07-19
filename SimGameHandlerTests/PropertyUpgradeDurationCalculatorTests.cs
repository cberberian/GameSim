using System.Collections.Generic;
using NUnit.Framework;
using SimGame.Handler.Calculators;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;

namespace SimGameHandlerTests
{
    [TestFixture]
    public class PropertyUpgradeDurationCalculatorTests
    {
//        [Test]
//        public void null_request_returns_non_null_result()
//        {
//            var calculator = new PropertyUpgradeDurationCalculator();
//            var ret = calculator.CalculateUpgradeTime(null);
//            Assert.IsNotNull(ret);
//        }
//
//        [Test]
//        public void null_propertyupgrade_returns_non_null_result()
//        {
//            var calculator = new PropertyUpgradeDurationCalculator();
//            var request = new PropertyUpgradeDurationCalculatorRequest();
//            var ret = calculator.CalculateUpgradeTime(request);
//            Assert.IsNotNull(ret);
//        }
//
//        [Test]
//        public void zero_metal_returns_total_time_0()
//        {
//            var calculator = new PropertyUpgradeDurationCalculator();
//            var request = new PropertyUpgradeDurationCalculatorRequest
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
//            var calculator = new PropertyUpgradeDurationCalculator();
//            var request = new PropertyUpgradeDurationCalculatorRequest
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
//            var calculator = new PropertyUpgradeDurationCalculator();
//            var request = new PropertyUpgradeDurationCalculatorRequest
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
//            var calculator = new PropertyUpgradeDurationCalculator();
//            var request = new PropertyUpgradeDurationCalculatorRequest
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
//            var calculator = new PropertyUpgradeDurationCalculator();
//            var request = new PropertyUpgradeDurationCalculatorRequest
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
////            var calculator = new PropertyUpgradeDurationCalculator();
////            var request = new PropertyUpgradeDurationCalculatorRequest
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