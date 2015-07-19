using System.Collections.Generic;
using System.Web.Http;
using SimGame.Domain;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using BuildingFacilityType = SimGame.Handler.Entities.Legacy.BuildingFacilityType;
using handler = SimGame.Handler;
using InventoryItem = MvcApplication1.Models.InventoryItem;

namespace MvcApplication1.Controllers
{
    public class BuildingFacilityTypeController : ApiController
    {
        #region fields

        private readonly InventoryItem _metal;

        private readonly InventoryItem _sugarAndSpice;

        private readonly InventoryItem _seeds;

        private readonly InventoryItem _shovel;

        private readonly InventoryItem _treeSapling;

        private readonly InventoryItem _fruitsAndBerries;

        private readonly InventoryItem _vegetables;

        private readonly InventoryItem _flourBag;

        private readonly InventoryItem _planks;

        private readonly InventoryItem _plastic;

        private readonly InventoryItem _textiles;

        private readonly InventoryItem _wood;

        private readonly InventoryItem _nails;

        private readonly InventoryItem _hammer;
        private readonly InventoryItem _minerals;
        private readonly InventoryItem _chemicals;
        private readonly InventoryItem _measuringTape;
        private readonly InventoryItem _glue;
        private readonly InventoryItem _glass;
        private readonly InventoryItem _cream;
        private readonly InventoryItem _animalFeed;

        #endregion

        public BuildingFacilityTypeController()
        {
            #region Factory

            _metal = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Metal",
                DurationInMinutes = 1,
                Prerequisites = {},
                ItemType = ProductTypeEnum.Metal,
            };

            _seeds = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Seeds",
                DurationInMinutes = 20,
                Prerequisites = { },
                ItemType = ProductTypeEnum.Seed,
            };

            _plastic = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Plastic",
                DurationInMinutes = 9,
                Prerequisites = { },
                ItemType = ProductTypeEnum.Plastic,
            };

            _textiles = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Textiles",
                DurationInMinutes = 180,
                Prerequisites = { },
                ItemType = ProductTypeEnum.Textiles,
            };

            _wood = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Wood",
                DurationInMinutes = 3,
                Prerequisites = { },
                ItemType = ProductTypeEnum.Wood,
            };

            _sugarAndSpice = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Sugar and Spice",
                DurationInMinutes = 240,
                Prerequisites = { },
                ItemType = ProductTypeEnum.SugarAndSpice
            };

            _minerals = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Minerals",
                DurationInMinutes = 30,
                Prerequisites = { },
                ItemType = ProductTypeEnum.Mineral,
            };

            _chemicals = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Chemicals",
                DurationInMinutes = 120,
                Prerequisites = { },
                ItemType = ProductTypeEnum.Chemical,
            };

            _animalFeed = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Animal Feed",
                DurationInMinutes = 360,
                Prerequisites = { },
                ItemType = ProductTypeEnum.AnimalFeed,
            };

            #endregion

            #region Hardware Store

            _shovel = new InventoryItem
            {
                FacilityType = BuildingFacilityType.HardwareStore,
                Name = "Shovel",
                DurationInMinutes = 30,
                Prerequisites = new[]
                {
                    new InventoryItem(_metal) {Quantity = 1},
                    new InventoryItem(_wood) {Quantity = 1},
                    new InventoryItem(_plastic) {Quantity = 1}
                },
                 ItemType = ProductTypeEnum.Shovel
            };

            _hammer = new InventoryItem
            {
                FacilityType = BuildingFacilityType.HardwareStore,
                Name = "Hammer",
                DurationInMinutes = 14,
                Prerequisites = new[]
                {
                    new InventoryItem(_metal) { Quantity = 1 }, 
                    new InventoryItem(_wood) { Quantity = 1 }

                },
                ItemType = ProductTypeEnum.Hammer
            };

            #endregion

            #region Build Supplies

            _planks = new InventoryItem
            {
                FacilityType = BuildingFacilityType.BuildingSuppliesStore,
                Name = "Planks",
                DurationInMinutes = 30,
                Prerequisites = new[]
                {
                    new InventoryItem(_wood) {Quantity = 2}
                },
                ItemType = ProductTypeEnum.Planks
            };

            _nails = new InventoryItem
            {
                FacilityType = BuildingFacilityType.BuildingSuppliesStore,
                Name = "Nails",
                DurationInMinutes = 5,
                Prerequisites = new[]
                {
                    new InventoryItem(_metal) {Quantity = 2}
                },
                ItemType = ProductTypeEnum.Nail
            };

            #endregion

            #region Gardening Supplies

            _treeSapling = new InventoryItem
            {
                FacilityType = BuildingFacilityType.GardeningSuppliesStore,
                Name = "Tree Sapling",
                DurationInMinutes = 90,
                Prerequisites = new[]
                {
                    new InventoryItem(_seeds) {Quantity = 2},
                    new InventoryItem(_shovel) {Quantity = 1}
                },
                ItemType = ProductTypeEnum.TreeSapling
            };

            #endregion

            #region Farmers Market

            _fruitsAndBerries = new InventoryItem
            {
                FacilityType = BuildingFacilityType.FarmersMarket,
                Name = "Fruits and Berries",
                DurationInMinutes = 90,
                Prerequisites = new[]
                {
                    new InventoryItem(_seeds) {Quantity = 2},
                    new InventoryItem(_treeSapling) {Quantity = 1}
                },
                ItemType = ProductTypeEnum.FruitsAndBerries
            };

            _vegetables = new InventoryItem
            {
                FacilityType = BuildingFacilityType.FarmersMarket,
                Name = "Vegetables",
                DurationInMinutes = 20,
                Prerequisites = new[]
                {
                    new InventoryItem(_seeds) {Quantity = 2},
                },
                ItemType = ProductTypeEnum.Vegetable
            };

            _flourBag = new InventoryItem
            {
                FacilityType = BuildingFacilityType.FarmersMarket,
                Name = "Flour Bag",
                DurationInMinutes = 30,
                Prerequisites = new[]
                {
                    new InventoryItem(_seeds) { Quantity = 2 }, 
                    new InventoryItem(_textiles) { Quantity = 2 }, 

                },
                ItemType = ProductTypeEnum.Flour
            };

            _cream = new InventoryItem
            {
                FacilityType = BuildingFacilityType.FarmersMarket,
                Name = "Cream",
                DurationInMinutes = 75,
                Prerequisites = new []
                {
                    new InventoryItem(_animalFeed) { Quantity = 1 }
                },
                ItemType = ProductTypeEnum.Cream
            };

            #endregion

            _measuringTape = new InventoryItem
            {
                FacilityType = BuildingFacilityType.HardwareStore,
                Name = "Measuring Tape",
                DurationInMinutes = 20,
                Prerequisites = new []
                {
                    new InventoryItem(_metal) { Quantity = 1 }, 
                    new InventoryItem(_plastic) { Quantity = 1 }
                },
                ItemType = ProductTypeEnum.MeasuringTape
            };
            _glue = new InventoryItem
            {
                FacilityType = BuildingFacilityType.BuildingSuppliesStore,
                Name = "Glue",
                DurationInMinutes = 60,
                Prerequisites = new []
                {
                    new InventoryItem(_chemicals) { Quantity = 2 },
                    new InventoryItem(_plastic) { Quantity = 1 }
                },
                ItemType = ProductTypeEnum.Glass
            };
            _glass = new InventoryItem
            {
                FacilityType = BuildingFacilityType.Factory,
                Name = "Glass",
                DurationInMinutes = 320,
                Prerequisites = {},
                ItemType = ProductTypeEnum.Glass
            };
        }

        // GET api/InventoryItems
        public IEnumerable<Models.BuildingFacilityType> Get()
        {
            return new[]
            {
                GetFactoryFacility(),
                GetHardwareStoreFacility(),
                GetBuildingSuppliesFacility(),
                GetFurnitureStoreFacility(),
                GetGardeningSuppliesFacility(),
                GetFarmersMarketFacility(),
                GetDonutShopFacility(),
                GetFashionStoreFacility()

            };
        }

        private Models.BuildingFacilityType GetFashionStoreFacility()
        {
            return new Models.BuildingFacilityType
            {
                Name = "Fashion Store",
                FacilityType = BuildingFacilityType.FashionStore,
                ParallelProcessing = false,
                SupportedItems = new[]
                {
                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.FashionStore,
                        Name = "Cap",
                        DurationInMinutes = 60,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_textiles) { Quantity = 2 },
                            new InventoryItem(_measuringTape) { Quantity = 1 }
                        },
                        ItemType = ProductTypeEnum.Cap
                    },

                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.FashionStore,
                        Name = "Shoes",
                        DurationInMinutes = 75,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_textiles) { Quantity = 2 },
                            new InventoryItem(_plastic) { Quantity = 1 },
                            new InventoryItem(_glue) { Quantity = 1 }
                        },
                        ItemType = ProductTypeEnum.Shoe
                    },

                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.FashionStore,
                        Name = "Watch",
                        DurationInMinutes = 90,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_glass) { Quantity = 1 },
                            new InventoryItem(_plastic) { Quantity = 2 },
                            new InventoryItem(_chemicals) { Quantity = 1 }
                        },
                        ItemType = ProductTypeEnum.Watch
                    }
                }
            };
        }

        // GET api/InventoryItems/5
        public Models.BuildingFacilityType Get(int id)
        {
            return new Models.BuildingFacilityType();
        }

        #region Helper Methods

        private Models.BuildingFacilityType GetDonutShopFacility()
        {

            return new Models.BuildingFacilityType
            {
                Name = "Donut Shop",
                FacilityType = BuildingFacilityType.DonutShop,
                ParallelProcessing = false,
                SupportedItems = new[]
                {
                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.DonutShop,
                        Name = "Donut",
                        DurationInMinutes = 45,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_flourBag) { Quantity = 1 },
                            new InventoryItem(_sugarAndSpice) { Quantity = 1 }
                        },
                        ItemType = ProductTypeEnum.Donut
                    },

                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.DonutShop,
                        Name = "Green Smoothie",
                        DurationInMinutes = 30,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_vegetables) { Quantity = 1 },
                            new InventoryItem(_fruitsAndBerries) { Quantity = 1 }
                        },
                        ItemType = ProductTypeEnum.GreenSmoothie
                    }
                }
            };
        }

        private Models.BuildingFacilityType GetFarmersMarketFacility()
        {

            return new Models.BuildingFacilityType
            {
                Name = "Farmers Market",
                FacilityType = BuildingFacilityType.FarmersMarket,
                ParallelProcessing = false,
                SupportedItems = new[]
                {
                    new InventoryItem(_vegetables) { Quantity = 1 },
                    new InventoryItem(_flourBag) { Quantity = 1 },
                    new InventoryItem(_fruitsAndBerries) { Quantity = 1 },
                    _cream
                }
            };
        }

        private Models.BuildingFacilityType GetGardeningSuppliesFacility()
        {
            return new Models.BuildingFacilityType
            {
                Name = "Garding Supplies",
                FacilityType = BuildingFacilityType.GardeningSuppliesStore,
                ParallelProcessing = false,
                SupportedItems = new[]
                {
                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.GardeningSuppliesStore,
                        Name = "Grass",
                        DurationInMinutes = 30,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_seeds) { Quantity = 1 },
                            new InventoryItem(_shovel) { Quantity = 1 },
                        },
                        ItemType = ProductTypeEnum.Grass
                    },

                    _treeSapling,
                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.GardeningSuppliesStore,
                        Name = "Garden Furniture",
                        DurationInMinutes = 135,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_planks) { Quantity = 2 },
                            new InventoryItem(_plastic) { Quantity = 2 },
                            new InventoryItem(_textiles) { Quantity = 2 }
                        },
                        ItemType = ProductTypeEnum.LawnFurniture
                    }

                }
            };
        }

        private Models.BuildingFacilityType GetFurnitureStoreFacility()
        {
            return new Models.BuildingFacilityType
            {
                Name = "Furniture Store",
                FacilityType = BuildingFacilityType.FurnitureStore,
                ParallelProcessing = false,
                SupportedItems = new[]
                {

                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.FurnitureStore,
                        Name = "Chair",
                        DurationInMinutes = 20,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_wood) { Quantity = 2 },
                            new InventoryItem(_nails) { Quantity = 1 },
                            new InventoryItem(_hammer) { Quantity = 1 }
                        },
                        ItemType = ProductTypeEnum.Chair
                    },

                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.FurnitureStore,
                        Name = "Table",
                        DurationInMinutes = 30,
                        Prerequisites = new[]
                        {
                            new InventoryItem(_planks) { Quantity = 1 },
                            new InventoryItem(_nails) { Quantity = 2 },
                            new InventoryItem(_hammer) { Quantity = 1 },
                        },
                        ItemType = ProductTypeEnum.Table
                    },

                }
            };
        }

        private Models.BuildingFacilityType GetBuildingSuppliesFacility()
        {
            return new Models.BuildingFacilityType
            {
                Name = "Building Supplies Store",
                FacilityType = BuildingFacilityType.BuildingSuppliesStore,
                ParallelProcessing = false,
                SupportedItems = new[]
                {
                    _nails,
                    _planks,
                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.BuildingSuppliesStore,
                        Name = "Bricks",
                        DurationInMinutes = 20,
                        Prerequisites = new []
                        {
                            new InventoryItem(_minerals) { Quantity = 2 }
                        },
                        ItemType = ProductTypeEnum.Brick
                    },
                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.BuildingSuppliesStore,
                        Name = "Cement",
                        DurationInMinutes = 50,
                        Prerequisites = new []
                        {
                            new InventoryItem(_minerals) { Quantity = 2 },
                            new InventoryItem(_chemicals) { Quantity = 2 }
                        },
                        ItemType = ProductTypeEnum.Cement
                    },
                    _glue,
                }
            };

        }

        private Models.BuildingFacilityType GetHardwareStoreFacility()
        {
            return new Models.BuildingFacilityType
            {
                Name = "Hardware Store",
                FacilityType = BuildingFacilityType.HardwareStore,
                ParallelProcessing = false,
                SupportedItems = new[]
                {
                    _hammer,

                    _measuringTape,
                    _shovel,
                    new InventoryItem
                    {
                        FacilityType = BuildingFacilityType.HardwareStore,
                        Name = "Cooking Utensils",
                        DurationInMinutes = 45,
                        Prerequisites = new []
                        {
                            new InventoryItem(_metal) { Quantity = 2 },
                            new InventoryItem(_plastic ) { Quantity = 2 },
                            new InventoryItem(_wood ) { Quantity = 2 }
                        },
                        ItemType = ProductTypeEnum.KitchenUtensils
                    }
                }
            };
        }

        private Models.BuildingFacilityType GetFactoryFacility()
        {
            return new Models.BuildingFacilityType
            {
                Name = "Factory",
                FacilityType = BuildingFacilityType.Factory,
                ParallelProcessing = true,
                SupportedItems = new[]
                {
                    _metal,
                    _wood,
                    _plastic,
                    _seeds,
                    _minerals,
                    _chemicals,
                    _textiles,
                    _sugarAndSpice,
                    _glass,
                    _animalFeed
                }
            };
        }

        #endregion

    }
}
