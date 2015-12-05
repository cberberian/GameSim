using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;
using SimGame.Handler.Calculators;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;
using SpecsFor;

namespace SimGameHandlerTests
{
    [TestFixture]
    public class RequiredProductFlattenerTests
    {
        #region given_prouduct_4_quantity_1

        public class given_prouduct_4_quantity_1 : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
                var buildingUpgrades = new[]
                {
                    new BuildingUpgrade
                    {
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = 4,
                                Quantity = 1
                            }
                        }
                    }
                };

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes
                });

            }

            [Test]
            public void then_products_should_equal_1()
            {
                _result.Products.Length.ShouldEqual(1);
            }

            [Test]
            public void then_products_quantity_should_equal_1()
            {
                _result.Products.First().Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_products_timeToFulfill_should_equal_10()
            {
                _result.Products.First().TimeToFulfill.ShouldEqual(10);
            }

            [Test]
            public void then_products_totalDuration_should_equal_10()
            {
                _result.Products.First().TotalDuration.ShouldEqual(10);
            }

            [Test]
            public void then_products_timeToFulfillPrequisites_should_equal_0()
            {
                _result.Products.First().TimeToFulfillPrerequisites.ShouldEqual(0);
            }


        }

        #endregion

        #region given_prouduct_3_quantity_1

        public class given_prouduct_3_quantity_1 : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
                var buildingUpgrades = new[]
                {
                    new BuildingUpgrade
                    {
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = 3,
                                Quantity = 1
                            }
                        }
                    }
                };

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes
                });

            }

            [Test]
            public void then_products_should_count_2()
            {
                _result.Products.Length.ShouldEqual(2);
            }

            #region Product 3 then

            [Test]
            public void then_productType3_quantity_should_equal_1()
            {
                _result.Products.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_productType3_timeToFulfill_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TimeToFulfill.ShouldEqual(10);
            }

            [Test]
            public void then_productType3_timeToFulfillPrequisites_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TimeToFulfillPrerequisites.ShouldEqual(10);
            }

            [Test]
            public void then_productType3_totalDuration_should_equal_20()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TotalDuration.ShouldEqual(20);
            }

            #endregion

            #region Product 4 then

            [Test]
            public void then_productType4_quantity_should_equal_1()
            {
                _result.Products.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_productType4_TimeToFulfill_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 4).TimeToFulfill.ShouldEqual(10);
            }

            [Test]
            public void then_productType4_TimeToFulfillPreRequisites_should_equal_0()
            {
                _result.Products.First(x => x.ProductTypeId == 4).TimeToFulfillPrerequisites.ShouldEqual(0);
            }

            [Test]
            public void then_productType4_TotalDuration_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 4).TimeToFulfill.ShouldEqual(10);
            }

            #endregion

        }

        #endregion

        #region given_prouduct_2_quantity_1

        public class given_prouduct_2_quantity_1 : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
                var buildingUpgrades = new[]
                {
                    new BuildingUpgrade
                    {
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = 2,
                                Quantity = 1
                            }
                        }
                    }
                };

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes
                });

            }

            /// <summary>
            ///     product 2 1x
            ///         product 3 1x 
            ///             requires 4 1x 
            ///         product 4 2x 
            ///     
            /// </summary>
            [Test]
            public void then_products_should_count_3()
            {
                _result.Products.Length.ShouldEqual(3);
            }

            [Test]
            public void then_productType2_quantity_should_equal_1()
            {
                _result.Products.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_productType2_timeToFulfill_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 2).TimeToFulfill.ShouldEqual(10);
            }

            [Test]
            public void then_productType2_timeToFulfillPrequisites_should_equal_50()
            {
                _result.Products.First(x => x.ProductTypeId == 2).TimeToFulfillPrerequisites.ShouldEqual(50);
            }

            [Test]
            public void then_productType2_totalDuration_should_equal_60()
            {
                _result.Products.First(x => x.ProductTypeId == 2).TotalDuration.ShouldEqual(60);
            }

            #region Product 3 then

            [Test]
            public void then_productType3_quantity_should_equal_1()
            {
                _result.Products.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_productType3_timeToFulfill_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TimeToFulfill.ShouldEqual(10);
            }

            [Test]
            public void then_productType3_timeToFulfillPrequisites_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TimeToFulfillPrerequisites.ShouldEqual(10);
            }

            [Test]
            public void then_productType3_totalDuration_should_equal_20()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TotalDuration.ShouldEqual(20);
            }

            #endregion

            #region Product 4 then

            [Test]
            public void then_productType4_quantity_should_equal_1()
            {
                _result.Products.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(3);
            }

            [Test]
            public void then_productType4_TimeToFulfill_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 4).TimeToFulfill.ShouldEqual(30);
            }

            [Test]
            public void then_productType4_TimeToFulfillPreRequisites_should_equal_0()
            {
                _result.Products.First(x => x.ProductTypeId == 4).TimeToFulfillPrerequisites.ShouldEqual(0);
            }

            [Test]
            public void then_productType4_TotalDuration_should_equal_10()
            {
                _result.Products.First(x => x.ProductTypeId == 4).TimeToFulfill.ShouldEqual(30);
            }

            #endregion
        }

        #endregion

        #region given_prouduct_1_quantity_1

        public class given_prouduct_1_quantity_1 : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
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

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes
                });

            }

            /// <summary>
            ///     product 1 1x
            ///         product 2 1x
            ///             product 3 1x
            ///                 product 4 1x
            ///             product 4 2x
            ///         product 3 2x
            ///             product 4 1x count 2 = 2
            ///         product 4 3x
            /// </summary>
            [Test]
            public void then_products_should_count_4()
            {
                _result.Products.Length.ShouldEqual(4);
            }

            [Test]
            public void then_productType1_quantity_should_equal_1()
            {
                _result.Products.First(x => x.ProductTypeId == 1).Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_productType2_quantity_should_equal_1()
            {
                _result.Products.First(x => x.ProductTypeId == 2).Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_productType3_quantity_should_equal_3()
            {
                _result.Products.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(3);
            }

            [Test]
            public void then_productType4_quantity_should_equal_6()
            {
                _result.Products.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(8);
            }
        }

        #endregion

        #region given_prouduct_4_quantity_1_in_storage_1

        public class given_prouduct_4_quantity_1_in_storage_1 : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
                var buildingUpgrades = new[]
                {
                    new BuildingUpgrade
                    {
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = 4,
                                Quantity = 1
                            }
                        }
                    }
                };

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes,
                    CityStorage = new CityStorage
                    {
                        CurrentInventory = new[]
                        {
                            new Product
                            {
                                ProductTypeId = 4,
                                Quantity = 1
                            }
                        }
                    }
                });

            }

            [Test]
            public void then_productType4_count_equals_0()
            {
                _result.Products.Length.ShouldEqual(0);
            }

        }

        #endregion

        #region given_prouduct_4_quantity_2_in_storage_1

        public class given_prouduct_4_quantity_2_in_storage_1 : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
                var buildingUpgrades = new[]
                {
                    new BuildingUpgrade
                    {
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = 4,
                                Quantity = 2
                            }
                        }
                    }
                };

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes,
                    CityStorage = new CityStorage
                    {
                        CurrentInventory = new[]
                        {
                            new Product
                            {
                                ProductTypeId = 4,
                                Quantity = 1
                            }
                        }
                    }
                });

            }

            [Test]
            public void then_products_should_equal_1()
            {
                _result.Products.Length.ShouldEqual(1);
            }

            [Test]
            public void then_products_quantity_should_equal_1()
            {
                _result.Products.First().Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_products_timeToFulfill_should_equal_10()
            {
                _result.Products.First().TimeToFulfill.ShouldEqual(10);
            }

            [Test]
            public void then_products_totalDuration_should_equal_10()
            {
                _result.Products.First().TotalDuration.ShouldEqual(10);
            }

            [Test]
            public void then_products_timeToFulfillPrequisites_should_equal_0()
            {
                _result.Products.First().TimeToFulfillPrerequisites.ShouldEqual(0);
            }

        }

        #endregion

        #region given_prouduct_3_quantity_1_in_storage_1

        public class given_prouduct_3_quantity_1_in_storage_1 : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
                var buildingUpgrades = new[]
                {
                    new BuildingUpgrade
                    {
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = 3,
                                Quantity = 1
                            }
                        }
                    }
                };

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes,
                    CityStorage = new CityStorage
                    {
                        CurrentInventory = new[]
                        {
                            new Product
                            {
                                ProductTypeId = 3,
                                Quantity = 1
                            }
                        }
                    }
                });

            }

            [Test]
            public void then_products_count_equals_0()
            {
                _result.Products.Length.ShouldEqual(0);
            }

        }

        #endregion

        #region given_prouduct_3_quantity_1_product4_1_in_storage

        public class given_prouduct_3_quantity_1_product4_1_in_storage : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
                var buildingUpgrades = new[]
                {
                    new BuildingUpgrade
                    {
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = 3,
                                Quantity = 1
                            }
                        }
                    }
                };

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes,
                    CityStorage = new CityStorage
                    {
                        CurrentInventory = new[]
                        {
                            new Product
                            {
                                ProductTypeId = 4,
                                Quantity = 1
                            }
                        }
                    }
                });

            }

            [Test]
            public void then_products_count_equals_1()
            {
                _result.Products.Length.ShouldEqual(1);
            }

            [Test]
            public void then_productTyp4_count_equals_0()
            {
                _result.Products.Any(x => x.ProductTypeId == 4).ShouldBeFalse();
            }

            [Test]
            public void then_productTyp3_quantity_equals_1()
            {
                _result.Products.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_productTyp3_timeToFulfill_equals_10()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TimeToFulfill.ShouldEqual(10);
            }

            [Test]
            public void then_productTyp3_timeToFulfillPrerequisites_equals_0()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TimeToFulfillPrerequisites.ShouldEqual(0);
            }

            [Test]
            public void then_productTyp3_totalDuration_equals_10()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TotalDuration.ShouldEqual(10);
            }

        }

        #endregion

        #region given_prouduct_3_quantity_2_product4_1_in_storage

        public class given_prouduct_3_quantity_2_product4_1_in_storage : SpecsFor<RequiredProductFlattener>
        {
            private RequiredProductFlattenerResponse _result;

            protected override void When()
            {
                var buildingUpgrades = new[]
                {
                    new BuildingUpgrade
                    {
                        Products = new List<Product>
                        {
                            new Product
                            {
                                ProductTypeId = 3,
                                Quantity = 2
                            }
                        }
                    }
                };

                var productTypes = FixtureHelper.GetHandlerProductTypes();

                _result = SUT.GetFlattenedInventory(new RequiredProductFlattenerRequest
                {
                    Products = buildingUpgrades.SelectMany(x => x.Products).ToArray(),
                    ProductTypes = productTypes,
                    CityStorage = new CityStorage
                    {
                        CurrentInventory = new[]
                        {
                            new Product
                            {
                                ProductTypeId = 4,
                                Quantity = 1
                            }
                        }
                    }
                });

            }

            [Test]
            public void then_products_count_equals_1()
            {
                _result.Products.Length.ShouldEqual(2);
            }

            #region Product Type 4 then

            [Test]
            public void then_productType4_count_equals_1()
            {
                _result.Products.Count(x => x.ProductTypeId == 4).ShouldEqual(1);
            }

            [Test]
            public void then_productType4_quantity_equals_1()
            {
                _result.Products.First(x => x.ProductTypeId == 4).Quantity.ShouldEqual(1);
            }

            [Test]
            public void then_productType4_timeToFulfill_equals_10()
            {
                _result.Products.First(x => x.ProductTypeId == 4).TimeToFulfill.ShouldEqual(10);
            }

            [Test]
            public void then_productType4_totalDuration_equals_10()
            {
                _result.Products.First(x => x.ProductTypeId == 4).TotalDuration.ShouldEqual(10);
            }

            #endregion

            #region ProductType3 then

            [Test]
            public void then_productType3_count_equals_2()
            {
                _result.Products.Count(x => x.ProductTypeId == 3).ShouldEqual(1);
            }

            [Test]
            public void then_productTyp3_quantity_equals_1()
            {
                _result.Products.First(x => x.ProductTypeId == 3).Quantity.ShouldEqual(2);
            }

            [Test]
            public void then_productTyp3_timeToFulfill_equals_20()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TimeToFulfill.ShouldEqual(20);
            }

            [Test]
            public void then_productTyp3_timeToFulfillPrerequisites_equals_10()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TimeToFulfillPrerequisites.ShouldEqual(10);
            }

            [Test]
            public void then_productTyp3_totalDuration_equals_10()
            {
                _result.Products.First(x => x.ProductTypeId == 3).TotalDuration.ShouldEqual(30);
            }

            #endregion

        }

        #endregion

    }
}