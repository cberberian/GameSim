using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GameSimContext>
    {
        private static ManufacturerType _factoryManufacturerType;
        private static ManufacturerType _hardwareStoreManufacturerType;
        private static ManufacturerType _buildingSuppliesStoreManufacturerType;
        private static ManufacturerType _furnitureStoreManufacturerType;
        private static ManufacturerType _gardeningSuppliesManufacturerType;
        private static ManufacturerType _farmersMarketManufacturerType;
        private static ManufacturerType _fashionStoreManufacturerType;
        private static ManufacturerType _donutShopManufacturerType;
        private static ManufacturerType _fastFoodRestaurantManufacturerType;
        private static ManufacturerType _homeAppliancesManufacturerType;
        private static ProductType _metalProductType;
        private static ProductType _wooProductType;
        private static ProductType _plasticProductType;
        private static ProductType _seedProductType;
        private static ProductType _mineralProductType;
        private static ProductType _chemicalProductType;
        private static ProductType _textileProductType;
        private static ProductType _sugarAndSpiceProductType;
        private static ProductType _glassProductType;
        private static ProductType _animalFeedProductType;
        private static ProductType _electricalComponentProductType;
        private static ProductType _hammerProductType;
        private static ProductType _measuringTapeProductType;
        private static ProductType _shovelProductType;
        private static ProductType _cookingUtensilsProductType;
        private static ProductType _drillProductType;
        private static ProductType _nailProductType;
        private static ProductType _plankProductType;
        private static ProductType _treeSaplingProductType;
        private static ProductType _glueProductType;
        private static ProductType _flourProductType;
        private static ProductType _vegetableProductType;
        private static ProductType _fruitsAndBerriesProductType;
        private static ProductType _creamProductType;
        private static ProductType _cheeseProductType;
        private static ProductType _cementProductType;
        private static ProductType _brickProductType;
        private static ProductType _beefProductType;
        


        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameSimContext context)
        {
            InitializeManufacturerTypes(context);
            InitializeManufacturers(context);
        }

        private static void InitializeManufacturers(GameSimContext context)
        {
            const int massProductionQueueSize = 4;
            context.Manufacturers.AddOrUpdate(x=>x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 1", GetManufacturerQueueSlots(massProductionQueueSize), _factoryManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 2", GetManufacturerQueueSlots(massProductionQueueSize), _factoryManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 3", GetManufacturerQueueSlots(massProductionQueueSize), _factoryManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 4", GetManufacturerQueueSlots(massProductionQueueSize), _factoryManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 5", GetManufacturerQueueSlots(massProductionQueueSize), _factoryManufacturerType, false));
            const int basicFactoryQueueSize = 3;
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 6", GetManufacturerQueueSlots(basicFactoryQueueSize), _factoryManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 7", GetManufacturerQueueSlots(basicFactoryQueueSize), _factoryManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 8", GetManufacturerQueueSlots(basicFactoryQueueSize), _factoryManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(massProductionQueueSize, "Mass Production Factory 9", GetManufacturerQueueSlots(basicFactoryQueueSize), _factoryManufacturerType, false));
            
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(5, "Hardware Store", GetManufacturerQueueSlots(5), _hardwareStoreManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(5, "Building Supplies", GetManufacturerQueueSlots(5), _buildingSuppliesStoreManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(4, "Furniture Store", GetManufacturerQueueSlots(4), _furnitureStoreManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(5, "Gardening Supplies Store", GetManufacturerQueueSlots(5), _gardeningSuppliesManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(6, "Farmers Market", GetManufacturerQueueSlots(6), _farmersMarketManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(4, "Fashion Store", GetManufacturerQueueSlots(4), _fashionStoreManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(4, "Donut Shop", GetManufacturerQueueSlots(4), _donutShopManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(3, "Fast Food Restaurant", GetManufacturerQueueSlots(3), _fastFoodRestaurantManufacturerType, false));
            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(2, "Home Appliances", GetManufacturerQueueSlots(2), _homeAppliancesManufacturerType, false));
//            var totalNumberOfProducts = GetTotalNumberOfProducts();
//            context.Manufacturers.AddOrUpdate(x => x.Id, CreateManufacturer(totalNumberOfProducts, "City Storage", GetManufacturerQueueSlots(totalNumberOfProducts), _donutShopManufacturerType, true));


        }

        private static int GetTotalNumberOfProducts()
        {
            return _buildingSuppliesStoreManufacturerType.ProductTypes.Count +
                   _donutShopManufacturerType.ProductTypes.Count +
                   _factoryManufacturerType.ProductTypes.Count +
                   _farmersMarketManufacturerType.ProductTypes.Count +
                   _fashionStoreManufacturerType.ProductTypes.Count +
                   _furnitureStoreManufacturerType.ProductTypes.Count +
                   _gardeningSuppliesManufacturerType.ProductTypes.Count +
                   _hardwareStoreManufacturerType.ProductTypes.Count;

        }

        private static List<ManufacturingQueueSlot> GetManufacturerQueueSlots(int numberOfSlots)
        {

            var manufacturingQueueSlots = new List<ManufacturingQueueSlot>();
            for (var a = 0; a < numberOfSlots; a++)
            {
                manufacturingQueueSlots.Add(new ManufacturingQueueSlot
                {
                    SlotNumber = a + 1
                });
            }
            return manufacturingQueueSlots;
        }

        private static Manufacturer CreateManufacturer(int queueSize, string massProductionFactory, List<ManufacturingQueueSlot> manufacturingQueueSlots, ManufacturerType factoryManufacturerType, bool isCityStorage)
        {
            var manufacturer = new Manufacturer
            {
                    
                ManufacturerTypeId = factoryManufacturerType.Id,
                QueueSize = queueSize,
                Description = massProductionFactory,
                IsCityStorage = isCityStorage
            };
            manufacturer.AddManufacturer(manufacturingQueueSlots);
            return manufacturer;
        }

        private static void InitializeManufacturerTypes(IGameSimContext context)
        {
            _factoryManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.Factory,
                Name = "Factory",
                HasFixedQueueSize = true,
                QueueSize = 2,
                SupportsParallelManufacturing = true
            }; //InitializeManufacturerType(GetFactoryProductTypes(), "Factory", true, 2, true);
            _hardwareStoreManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.HardwareStore,
                Name = "Hardware Store",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetHardwareStoreProductTypes(), "Hardware Store", false, 2, false);
            _buildingSuppliesStoreManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.BuildingSupplies,
                Name = "Building Supplies",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetBuildingSupplyStoreProductTypes(), "Building Supplies Store", false, 2, false);
            _furnitureStoreManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.FurnitureStore,
                Name = "Furniture Store",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetFurnitureStoreProductTypes(), "Furniture Store", false, 2, false);
            _gardeningSuppliesManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.GardeningSupplies,
                Name = "Gardening Supplies",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetGardeningSuppliesProductTypes(), "Gardening Supplies", false, 2, false);
            _farmersMarketManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.FarmersMarket,
                Name = "Farmers Market",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetFarmersMarketProductTypes(), "Farmer's Market", false, 2, false);
            _fashionStoreManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.FashionStore,
                Name = "Fashion Store",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetFashionStoreProductTypes(), "Fashion Store", false, 2, false);
            _donutShopManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.DonutShop,
                Name = "Donut Shop",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetDonutShopProductTypes(), "Donut Shop", false, 2, false);
            _fastFoodRestaurantManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.FastFoodRestaurant,
                Name = "Fast Food Restaurant",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetDonutShopProductTypes(), "Donut Shop", false, 2, false);
            _homeAppliancesManufacturerType = new ManufacturerType
            {
                Id = (int)ManufacturerTypeEnum.HomeAppliances,
                Name = "Home Appliances",
                HasFixedQueueSize = false,
                QueueSize = 2,
                SupportsParallelManufacturing = false
            };// InitializeManufacturerType(GetDonutShopProductTypes(), "Donut Shop", false, 2, false);

            _factoryManufacturerType.ProductTypes = GetFactoryProductTypes();
            _hardwareStoreManufacturerType.ProductTypes = GetHardwareStoreProductTypes();
            _buildingSuppliesStoreManufacturerType.ProductTypes = GetBuildingSupplyStoreProductTypes();
            _furnitureStoreManufacturerType.ProductTypes = GetFurnitureStoreProductTypes();
            _gardeningSuppliesManufacturerType.ProductTypes = GetGardeningSuppliesProductTypes();
            _farmersMarketManufacturerType.ProductTypes = GetFarmersMarketProductTypes();
            _fashionStoreManufacturerType.ProductTypes = GetFashionStoreProductTypes();
            _donutShopManufacturerType.ProductTypes = GetDonutShopProductTypes();
            _fastFoodRestaurantManufacturerType.ProductTypes = GetFastFoodRestaurantProductTypes();
            _homeAppliancesManufacturerType.ProductTypes = GetHomeApplianceProductTypes();
            context.ManufacturerTypes.AddOrUpdate(x => x.Id,
                _factoryManufacturerType,
                _hardwareStoreManufacturerType,
                _buildingSuppliesStoreManufacturerType,
                _furnitureStoreManufacturerType,
                _gardeningSuppliesManufacturerType,
                _farmersMarketManufacturerType,
                _fashionStoreManufacturerType,
                _donutShopManufacturerType,
                _fastFoodRestaurantManufacturerType,
                _homeAppliancesManufacturerType
             );
            //Add City Storage defaults. 
            foreach (var ptype in _factoryManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true, 
                    ProductTypeId = ptype.Id
                });
            }
            foreach (var ptype in _hardwareStoreManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }
            foreach (var ptype in _buildingSuppliesStoreManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }
            foreach (var ptype in _furnitureStoreManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }
            foreach (var ptype in _gardeningSuppliesManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }

            foreach (var ptype in _farmersMarketManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }

            foreach (var ptype in _fashionStoreManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }

            foreach (var ptype in _donutShopManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }

            foreach (var ptype in _fastFoodRestaurantManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }

            foreach (var ptype in _homeAppliancesManufacturerType.ProductTypes)
            {
                context.Products.Add(new Product
                {
                    IsCityStorage = true,
                    ProductTypeId = ptype.Id
                });
            }

        }

        #region Home Appliances

        private static ICollection<ProductType> GetHomeApplianceProductTypes()
        {
            return new List<ProductType>
            {
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.HomeAppliances,
                    Id = (int) ProductTypeEnum.BBQGrill,
                    Name = "BBQ Grill",
                    TimeToManufacture = 165,
                    RequiredProducts = RequiredBBQGrillProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.HomeAppliances,
                    Id = (int) ProductTypeEnum.Refrigerator,
                    Name = "Refrigerator",
                    TimeToManufacture = 210,
                    RequiredProducts = RequiredRefrigeratorProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.HomeAppliances,
                    Id = (int) ProductTypeEnum.LightingSystem,
                    Name = "Lighting System",
                    TimeToManufacture = 210,
                    RequiredProducts = RequiredLightingSystemProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.HomeAppliances,
                    Id = (int) ProductTypeEnum.TV,
                    Name = "TV",
                    TimeToManufacture = 210,
                    RequiredProducts = RequiredTvProducts()
                }
            };
        }

        private static ICollection<Product> RequiredTvProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Plastic
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Glass
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.ElectricalComponent
                }
            };
        }

        private static ICollection<Product> RequiredLightingSystemProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Chemical
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.ElectricalComponent
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Glass
                }
            };
        }

        private static ICollection<Product> RequiredRefrigeratorProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Plastic
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Chemical
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.ElectricalComponent
                }
            };
        }

        private static ICollection<Product> RequiredBBQGrillProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 3,
                    ProductTypeId = (int) ProductTypeEnum.Metal
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.KitchenUtensils
                }
            };
        }

        #endregion

        #region Fast Food Restaurant

        private static ICollection<ProductType> GetFastFoodRestaurantProductTypes()
        {
            return new List<ProductType>
            {
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.FastFoodRestaurant,
                    Id = (int) ProductTypeEnum.IceCreamSandwich,
                    Name = "Ice Cream Sandwich",
                    TimeToManufacture = 45,
                    RequiredProducts = RequiredIceCreamSandwichProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.FastFoodRestaurant,
                    Id = (int) ProductTypeEnum.Pizza,
                    Name = "Pizza",
                    TimeToManufacture = 30,
                    RequiredProducts = RequiredPizzaProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.FastFoodRestaurant,
                    Id = (int) ProductTypeEnum.Burger,
                    Name = "Burger",
                    TimeToManufacture = 30,
                    RequiredProducts = RequiredBurgerProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.FastFoodRestaurant,
                    Id = (int) ProductTypeEnum.CheeseFries,
                    Name = "Cheese Fries",
                    TimeToManufacture = 20,
                    RequiredProducts = RequiredCheeseFriesProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int) ManufacturerTypeEnum.FastFoodRestaurant,
                    Id = (int) ProductTypeEnum.LemonadeBottle,
                    Name = "Lemonade Bottle",
                    TimeToManufacture = 60,
                    RequiredProducts = RequiredLemonadeBottleProducts()
                }
            };
        }

        private static ICollection<Product> RequiredLemonadeBottleProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Glass
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.SugarAndSpice // _creamProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.FruitsAndBerries // _creamProductType
                }
            };
        }

        private static ICollection<Product> RequiredCheeseFriesProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Vegetable
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Cheese // _creamProductType
                }
            };
        }

        private static ICollection<Product> RequiredBurgerProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Beef
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.BBQGrill // _creamProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.BreadRoll // _sugarAndSpiceProductType
                }
            };
        }

        private static ICollection<Product> RequiredPizzaProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Flour
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Cheese // _creamProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Beef // _sugarAndSpiceProductType
                }
            };
        }

        private static ICollection<Product> RequiredIceCreamSandwichProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.BreadRoll
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Cream // _creamProductType
                }
            };
        }

        #endregion

        #region Donut Shop

        private static ICollection<ProductType> GetDonutShopProductTypes()
        {
            return new List<ProductType>
            {
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.DonutShop,
                    Id = (int) ProductTypeEnum.Donut,
                    Name = "Donut",
                    TimeToManufacture = 45,
                    RequiredProducts = RequiredDonutProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.DonutShop,
                    Id = (int) ProductTypeEnum.GreenSmoothie,
                    Name = "Green Smoothie",
                    TimeToManufacture = 30,
                    RequiredProducts = RequiredGreenSmoothieProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.DonutShop,
                    Id = (int) ProductTypeEnum.BreadRoll,
                    Name = "Bread Roll",
                    TimeToManufacture = 60,
                    RequiredProducts = RequiredBreadRollProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.DonutShop,
                    Id = (int) ProductTypeEnum.CherryCheeseCake,
                    Name = "Cherry Cheese Cake",
                    TimeToManufacture = 90,
                    RequiredProducts = RequiredCherryCheeseCakeProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.DonutShop,
                    Id = (int) ProductTypeEnum.FrozenYogurt,
                    Name = "Frozen Yogurt",
                    TimeToManufacture = 240,
                    RequiredProducts = RequiredFrozenYogurtProducts()
                }
            };
        }

        #region Required Donut Shop Products

        private static List<Product> RequiredFrozenYogurtProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.FruitsAndBerries
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Cream // _creamProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.SugarAndSpice // _sugarAndSpiceProductType
                }
            };
        }

        private static List<Product> RequiredCherryCheeseCakeProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Flour
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.FruitsAndBerries
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Cheese
                }
            };
        }

        private static List<Product> RequiredBreadRollProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Flour
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Cream
                }
            };
        }

        private static List<Product> RequiredGreenSmoothieProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.FruitsAndBerries //fruitsAndBerriesProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Vegetable //vegetableProductType
                }
            };
        }

        private static List<Product> RequiredDonutProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Flour //flourProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.SugarAndSpice //sugarAndSpiceProductType
                }
            };
        }

        #endregion

        #endregion

        #region Fashion Store

        private static ICollection<ProductType> GetFashionStoreProductTypes()
        {
            return new List<ProductType>
            {
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.FashionStore,
                    Id = (int) ProductTypeEnum.Cap,
                    Name = "Cap",
                    TimeToManufacture = 60,
                    RequiredProducts = RequiredCapProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.FashionStore,
                    Id = (int) ProductTypeEnum.Shoe,
                    Name = "Shoes",
                    TimeToManufacture = 75,
                    RequiredProducts = RequiredShoeProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.FashionStore,
                    Id = (int) ProductTypeEnum.Watch,
                    Name = "Watch",
                    TimeToManufacture = 90,
                    RequiredProducts = RequiredWatchProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.FashionStore,
                    Id = (int) ProductTypeEnum.BusinessSuit,
                    Name = "Business Suit",
                    TimeToManufacture = 210,
                    RequiredProducts = RequiredBusinessSuitProducts()
                }
            };
        }

        private static ICollection<Product> RequiredBusinessSuitProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 3,
                    ProductTypeId = (int) ProductTypeEnum.Textiles //glassProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.MeasuringTape //plasticProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Glue //chemicalProductType
                }
            };
        }

        private static List<Product> RequiredWatchProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Glass //glassProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Plastic //plasticProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Chemical //chemicalProductType
                }
            };
        }

        private static List<Product> RequiredShoeProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Textiles //_textileProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Plastic // _plasticProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Glue // _glueProductType
                }
            };
        }

        private static List<Product> RequiredCapProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Textiles // _textileProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.MeasuringTape // _measuringTapeProductType
                }
            };
        }

        #endregion

        #region Farmers Market

        private static ICollection<ProductType> GetFarmersMarketProductTypes()
        {
            _flourProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.FarmersMarket,
                Id = (int) ProductTypeEnum.Flour,
                Name = "Flour",
                TimeToManufacture = 30,
                RequiredProducts = RequiredFlourProducts()
            };
            _vegetableProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.FarmersMarket,
                Id = (int) ProductTypeEnum.Vegetable,
                Name = "Vegetables",
                TimeToManufacture = 20,
                RequiredProducts = RequiredVegetableProducts()
            };
            _fruitsAndBerriesProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.FarmersMarket,
                Id = (int) ProductTypeEnum.FruitsAndBerries,
                Name = "Fruits and Berries",
                TimeToManufacture = 90,
                RequiredProducts = RequiredFruitAndBerryProducts()
            };
            _creamProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.FarmersMarket,
                Id = (int) ProductTypeEnum.Cream,
                Name = "Cream",
                TimeToManufacture = 75,
                RequiredProducts = RequiredCreamProducts()
            };
            _cheeseProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.FarmersMarket,
                Id = (int) ProductTypeEnum.Cheese,
                Name = "Cheese",
                TimeToManufacture = 105,
                RequiredProducts = RequiredCheeseProducts()
            };
            _beefProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.FarmersMarket,
                Id = (int) ProductTypeEnum.Beef,
                Name = "Beef",
                TimeToManufacture = 150,
                RequiredProducts = RequiredBeefProducts()
            };
            return new List<ProductType>
            {
                _vegetableProductType,
                _flourProductType,
                _fruitsAndBerriesProductType,
                _creamProductType,
                _cheeseProductType,
                _beefProductType
            };
        }

        private static List<Product> RequiredBeefProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 3,
                    ProductTypeId = (int) ProductTypeEnum.AnimalFeed //animalFeedProductType
                }
            };
        }

        private static List<Product> RequiredCheeseProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.AnimalFeed //animalFeedProductType
                }
            };
        }

        private static List<Product> RequiredCreamProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.AnimalFeed //animalFeedProductType
                }
            };
        }

        private static List<Product> RequiredFruitAndBerryProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Seed //seedProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.TreeSapling //treeSaplingProductType
                }
            };
        }

        private static List<Product> RequiredVegetableProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Seed //seedProductType
                }
            };
        }

        private static List<Product> RequiredFlourProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Seed //seedProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Textiles //textileProductType
                }
            };
        }

        #endregion

        #region Gardening Supplies

        private static ICollection<ProductType> GetGardeningSuppliesProductTypes()
        {
            _treeSaplingProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.GardeningSupplies,
                Id = (int) ProductTypeEnum.TreeSapling,
                Name = "Tree Sapling",
                TimeToManufacture = 90,
                RequiredProducts = RequiredTreeSapplingProducts()
            };
            return new Collection<ProductType>
            {
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.GardeningSupplies,
                    Id = (int) ProductTypeEnum.Grass,
                    Name = "Grass",
                    TimeToManufacture = 30,
                    RequiredProducts = RequiredGrassProducts()
                },
                _treeSaplingProductType,
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.GardeningSupplies,
                    Id = (int) ProductTypeEnum.LawnFurniture,
                    Name = "Garden Furniture",
                    TimeToManufacture = 135,
                    RequiredProducts = RequiredGardenFurnitureProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.GardeningSupplies,
                    Id = (int) ProductTypeEnum.FirePit,
                    Name = "Fire Pit",
                    TimeToManufacture = 240,
                    RequiredProducts = RequiredFirePitProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.GardeningSupplies,
                    Id = (int) ProductTypeEnum.GardenGnome,
                    Name = "Garden Gnome",
                    TimeToManufacture = 90,
                    RequiredProducts = RequiredGardenGnomeProducts()
                }
            };
        }

        private static ICollection<Product> RequiredGardenGnomeProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Cement //brickProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Glue //shovelProductType
                }
            };
        }

        private static List<Product> RequiredFirePitProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Brick //brickProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Shovel //shovelProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Cement //cementProductType
                }
            };
        }

        private static List<Product> RequiredGardenFurnitureProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Planks //plankProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Plastic //plasticProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Textiles //textileProductType
                }
            };
        }

        private static List<Product> RequiredGrassProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Seed //seedProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Shovel //shovelProductType
                }
            };
        }

        private static List<Product> RequiredTreeSapplingProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Seed //seedProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Shovel //shovelProductType
                }
            };
        }

        #endregion

        #region Furniture Store

        private static ICollection<ProductType> GetFurnitureStoreProductTypes()
        {
            return new Collection<ProductType>
            {
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.FurnitureStore,
                    Id = (int) ProductTypeEnum.Chair,
                    Name = "Chair",
                    TimeToManufacture = 20,
                    RequiredProducts = RequiredChairProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.FurnitureStore,
                    Id = (int) ProductTypeEnum.Table,
                    Name = "Table",
                    TimeToManufacture = 30,
                    RequiredProducts = RequiredTableProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.FurnitureStore,
                    Id = (int) ProductTypeEnum.HomeTextiles,
                    Name = "Home Textiles",
                    TimeToManufacture = 75,
                    RequiredProducts = RequiredHomeTextilesProducts()
                },
                new ProductType
                {
                    ManufacturerTypeId = (int)ManufacturerTypeEnum.FurnitureStore,
                    Id = (int) ProductTypeEnum.Couch,
                    Name = "Couch",
                    TimeToManufacture = 150,
                    RequiredProducts = RequiredCouchProducts()
                }
            };
        }

        private static ICollection<Product> RequiredCouchProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 3,
                    ProductTypeId = (int) ProductTypeEnum.Textiles //measuringTapeProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Drill //textileProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Glue //textileProductType
                }
            };
        }

        private static List<Product> RequiredHomeTextilesProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.MeasuringTape //measuringTapeProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Textiles //textileProductType
                }
            };
        }

        private static List<Product> RequiredTableProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Planks //plankProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Nail //nailProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Hammer //hammerProductType
                }
            };
        }

        private static List<Product> RequiredChairProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Wood //wooProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Nail //nailProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Hammer //hammerProductType
                }
            };
        }

        #endregion

        #region Building Supplies

        private static Collection<ProductType> GetBuildingSupplyStoreProductTypes()
        {
            _nailProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.BuildingSupplies,
                Id = (int) ProductTypeEnum.Nail,
                Name = "Nails",
                TimeToManufacture = 5,
                RequiredProducts = RequiredNailProducts()
            };
            _plankProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.BuildingSupplies,
                Id = (int) ProductTypeEnum.Planks,
                Name = "Planks",
                TimeToManufacture = 30,
                RequiredProducts = RequiredPlankProducts()
            };
            _glueProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.BuildingSupplies,
                Id = (int) ProductTypeEnum.Glue,
                Name = "Glue",
                TimeToManufacture = 60,
                RequiredProducts = RequiredGlueProducts()
            };
            _cementProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.BuildingSupplies,
                Id = (int) ProductTypeEnum.Cement,
                Name = "Cement",
                TimeToManufacture = 50,
                RequiredProducts = RequiredCementProducts()
            };
            _brickProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.BuildingSupplies,
                Id = (int) ProductTypeEnum.Brick,
                Name = "Bricks",
                TimeToManufacture = 20,
                RequiredProducts = RequiredBrickProducts()
            };
            return new Collection<ProductType>
            {
                _nailProductType,
                _plankProductType,
                _brickProductType,
                _cementProductType,
                _glueProductType
            };
        }

        private static List<Product> RequiredBrickProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Mineral //mineralProductType
                }
            };
        }

        private static List<Product> RequiredCementProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Mineral //mineralProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Chemical //chemicalProductType
                }
            };
        }

        private static List<Product> RequiredGlueProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Chemical //chemicalProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Plastic //plasticProductType
                }
            };
        }

        private static List<Product> RequiredPlankProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Wood //wooProductType
                }
            };
        }

        private static List<Product> RequiredNailProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Metal //metalProductType
                }
            };
        }

        #endregion

        #region Hardware Store

        private static Collection<ProductType> GetHardwareStoreProductTypes()
        {
            _hammerProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.HardwareStore,
                Id = (int) ProductTypeEnum.Hammer,
                Name = "Hammer",
                TimeToManufacture = 14,
                RequiredProducts = RequiredHammerProducts()
            };
            _measuringTapeProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.HardwareStore,
                Id = (int) ProductTypeEnum.MeasuringTape,
                Name = "Measuring Tape",
                TimeToManufacture = 20,
                RequiredProducts = RequiredMeasuringTapeProducts()
            };
            _shovelProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.HardwareStore,
                Id = (int) ProductTypeEnum.Shovel,
                Name = "Shovel",
                TimeToManufacture = 30,
                RequiredProducts = RequiredShovelProducts()
            };
            _cookingUtensilsProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.HardwareStore,
                Id = (int) ProductTypeEnum.KitchenUtensils,
                Name = "Cooking Utensils",
                TimeToManufacture = 45,
                RequiredProducts = RequiredCookingUtensilsProducts()
            };
            _drillProductType = new ProductType
            {
                Id = (int)ProductTypeEnum.Drill,
                ManufacturerTypeId = (int)ManufacturerTypeEnum.HardwareStore,
                Name = "Drill",
                TimeToManufacture = 60,
                RequiredProducts = RequiredDrillProducts()
            };
            return new Collection<ProductType>
            {
                _hammerProductType,
                _measuringTapeProductType,
                _shovelProductType,
                _cookingUtensilsProductType,
                _drillProductType
            };
        }

        private static ICollection<Product> RequiredDrillProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Metal //metalProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Plastic //wooProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.ElectricalComponent //plasticProductType
                }

            };
        }

        private static List<Product> RequiredCookingUtensilsProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Metal //metalProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Wood //wooProductType
                },
                new Product
                {
                    Quantity = 2,
                    ProductTypeId = (int) ProductTypeEnum.Plastic //plasticProductType
                }

            };
        }

        private static List<Product> RequiredShovelProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Metal //metalProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Wood //wooProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Plastic //plasticProductType
                }

            };
        }

        private static List<Product> RequiredMeasuringTapeProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Metal //metalProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Plastic //plasticProductType
                }

            };
        }

        private static List<Product> RequiredHammerProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Metal //metalProductType
                },
                new Product
                {
                    Quantity = 1,
                    ProductTypeId = (int) ProductTypeEnum.Wood //wooProductType
                }

            };
        }

        #endregion

        private static Collection<ProductType> GetFactoryProductTypes()
        {
            _metalProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.Metal,
                Name = "Metal",
                TimeToManufacture = 1
            };
            _wooProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.Wood,
                Name = "Wood",
                TimeToManufacture = 3
            };
            _plasticProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.Plastic,
                Name = "Plastic",
                TimeToManufacture = 9
            };
            _seedProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.Seed,
                Name = "Seeds",
                TimeToManufacture = 20
            };
            _mineralProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.Mineral,
                Name = "Minerals",
                TimeToManufacture = 30
            };
            _chemicalProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.Chemical,
                Name = "Chemicals",
                TimeToManufacture = 120
            };
            _textileProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.Textiles,
                Name = "Textiles",
                TimeToManufacture = 180
            };
            _sugarAndSpiceProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.SugarAndSpice,
                Name = "Sugar and Spice",
                TimeToManufacture = 240
            };
            _glassProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.Glass,
                Name = "Glass",
                TimeToManufacture = 300
            };
            _animalFeedProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.AnimalFeed,
                Name = "Animal Feed",
                TimeToManufacture = 360
            };
            _electricalComponentProductType = new ProductType
            {
                ManufacturerTypeId = (int)ManufacturerTypeEnum.Factory,
                Id = (int)ProductTypeEnum.ElectricalComponent,
                Name = "Electrical Components",
                TimeToManufacture = 420
            };
            return new Collection<ProductType>
            {
                _metalProductType,
                _wooProductType,
                _plasticProductType,
                _seedProductType,
                _mineralProductType,
                _chemicalProductType,
                _textileProductType,
                _sugarAndSpiceProductType,
                _glassProductType,
                _animalFeedProductType,
                _electricalComponentProductType
            };
        }
    }
}
