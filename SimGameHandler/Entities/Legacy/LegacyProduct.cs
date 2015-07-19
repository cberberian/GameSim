using System;
using System.Collections.Generic;
using SimGame.Domain;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class LegacyProduct : IProduct
    {
        private IEnumerable<IProduct> _preRequisites;

        protected LegacyProduct()
        {
        }

        protected LegacyProduct(IProduct legacyProduct, InventoryItemResolverOptions options)
        {
            if (options.MapQuantity)
                Quantity = legacyProduct.Quantity;
            
        }

        public virtual string Name
        {
            get { return GetType().Name; }
            set { }
        }
        public int? Quantity { get; set; }
        public virtual int? TimeToFulfill { get; set; }
        public virtual int? ProductTypeId { get; set; }
        public virtual int FacilityType { get; set; }
        public BuildingUpgrade BuildingUpgrade { get; set; }
        public int? TotalDuration { get; set; }
        public virtual IEnumerable<IProduct> PreRequisites
        {
            get { return _preRequisites ?? (_preRequisites = new List<IProduct>()); }
            set { _preRequisites = new List<IProduct>(value); }
        }

        public int ManufacturerTypeId { get; set; }

        public static LegacyProduct ResolveItem(IProduct legacyProduct, InventoryItemResolverOptions options)
        {
            
            // ReSharper disable once PossibleInvalidOperationException
            switch ((ProductTypeEnum)legacyProduct.ProductTypeId)
            {
                case ProductTypeEnum.Metal:
                    return new Metal(legacyProduct, options);
                case ProductTypeEnum.Wood:
                    return new Wood(legacyProduct, options);
                case ProductTypeEnum.Plastic:
                    return new Plastic(legacyProduct, options);
                case ProductTypeEnum.Seed:
                    return new Seed(legacyProduct, options);
                case ProductTypeEnum.Mineral:
                    return new Mineral(legacyProduct, options);
                case ProductTypeEnum.Chemical:
                    return new Chemical(legacyProduct, options);
                case ProductTypeEnum.Textiles:
                    return new Textiles(legacyProduct, options);
                case ProductTypeEnum.SugarAndSpice:
                    return new SugarAndSpice(legacyProduct, options);
                case ProductTypeEnum.Glass:
                    return new Glass(legacyProduct, options);
                case ProductTypeEnum.AnimalFeed:
                    return new AnimalFeed(legacyProduct, options);
                case ProductTypeEnum.Hammer:
                    return new Hammer(legacyProduct, options);
                case ProductTypeEnum.MeasuringTape:
                    return new MeasuringTape(legacyProduct, options);
                case ProductTypeEnum.Shovel:
                    return new Shovel(legacyProduct, options);
                case ProductTypeEnum.KitchenUtensils:
                    return new KitchenUtensils(legacyProduct, options);
                case ProductTypeEnum.Nail:
                    return new Nail(legacyProduct, options);
                case ProductTypeEnum.Planks:
                    return new Plank(legacyProduct, options);
                case ProductTypeEnum.Brick:
                    return new Brick(legacyProduct, options);
                case ProductTypeEnum.Cement:
                    return new Cement(legacyProduct, options);
                case ProductTypeEnum.Glue:
                    return new Glue(legacyProduct, options);
                case ProductTypeEnum.Chair:
                    return new Chair(legacyProduct, options);
                case ProductTypeEnum.Table:
                    return new Table(legacyProduct, options);
                case ProductTypeEnum.Grass:
                    return new Grass(legacyProduct, options);
                case ProductTypeEnum.TreeSapling:
                    return new TreeSapling(legacyProduct, options);
                case ProductTypeEnum.LawnFurniture:
                    return new LawnFurniture(legacyProduct, options);
                case ProductTypeEnum.Vegetable:
                    return new Vegetable(legacyProduct, options);
                case ProductTypeEnum.Flour:
                    return new Flour(legacyProduct, options);
                case ProductTypeEnum.FruitsAndBerries:
                    return new FruitsAndBerries(legacyProduct, options);
                case ProductTypeEnum.Cream:
                    return new Cream(legacyProduct, options);
                case ProductTypeEnum.Donut:
                    return new Donut(legacyProduct, options);
                case ProductTypeEnum.GreenSmoothie:
                    return new GreenSmoothie(legacyProduct, options);
                case ProductTypeEnum.Cap:
                    return new Cap(legacyProduct, options);
                case ProductTypeEnum.Shoe:
                    return new Shoe(legacyProduct, options);
                case ProductTypeEnum.Watch:
                    return new Watch(legacyProduct, options);
                default:
                    throw new ApplicationException("No Implementation for Item Type " + legacyProduct.ProductTypeId);

            }
        }
    }

    public class Watch : LegacyProduct
    {
        public Watch()
        {
        }

        public Watch(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int FacilityType
        {
            get { return BuildingFacilityType.FashionStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Watch; }
            set { }
        }

        public override int? TimeToFulfill
        {
            get { return 90; }
            set { }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Glass { Quantity = 1 }, 
                    new Plastic { Quantity = 2 }, 
                    new Chemical { Quantity = 1 }
                };
            }
            set { }
        }
    }

    public class Shoe : LegacyProduct
    {
        public Shoe()
        {
        }

        public Shoe(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int FacilityType
        {
            get { return BuildingFacilityType.FashionStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Shoe; }
            set { }
        }

        public override int? TimeToFulfill
        {
            get { return 75; }
            set { }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Textiles { Quantity = 2 }, 
                    new Plastic { Quantity = 1 }, 
                    new Glue { Quantity = 1 }
                };
            }
            set { }
        }
    }

    public class Cap : LegacyProduct
    {
        protected Cap()
        {
        }

        public Cap(IProduct legacyProduct, InventoryItemResolverOptions options) : base(legacyProduct, options)
        {
            
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.FashionStore; }
            set {  }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Cap; }
            set {  }
        }

        public override int? TimeToFulfill
        {
            get { return 60; }
            set { }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get 
            { 
                return new LegacyProduct[]
                {
                    new Textiles { Quantity = 2 }, 
                    new MeasuringTape { Quantity = 1 }
                }; 
            }
            set {  }
        }
    }

    public class Donut : LegacyProduct
    {
        protected Donut()
        {
        }

        public Donut(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 45; }
            set {  }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.DonutShop; }
            set {  }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Donut; }
            set { }
        }

        public override string Name
        {
            get { return "Donut"; }
            set {  }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Flour{ Quantity = 1 }, 
                    new SugarAndSpice { Quantity = 1 } 
                };
            }
            set { base.PreRequisites = value; }
        }
    }

    public class Cream : LegacyProduct
    {
        protected Cream()
        {
        }

        public Cream(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 75; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.FarmersMarket; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Cream; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new AnimalFeed { Quantity = 1 }
                };
            }
            set { }
        }

    }

    public class FruitsAndBerries : LegacyProduct
    {
        public FruitsAndBerries()
        {
        }

        public FruitsAndBerries(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 90; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.FarmersMarket; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.FruitsAndBerries; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Seed { Quantity = 2 },
                    new TreeSapling { Quantity = 1 }
                };
            }
            set { }
        }
    }

    public class Flour : LegacyProduct
    {
        public Flour()
        {
        }

        public Flour(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 30; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.FarmersMarket; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Flour; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Seed { Quantity = 2 },
                    new Textiles { Quantity = 2 }
                };
            }
            set { }
        }
    }

    public class LawnFurniture : LegacyProduct
    {
        protected LawnFurniture()
        {
        }

        public LawnFurniture(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 135; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.GardeningSuppliesStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.LawnFurniture; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Plank { Quantity = 2 },
                    new Plastic { Quantity = 2 },
                    new Textiles { Quantity = 2 }
                };
            }
            set { }
        }

    }

    public class TreeSapling : LegacyProduct
    {
        public TreeSapling()
        {
        }

        public TreeSapling(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 90; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.GardeningSuppliesStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.TreeSapling; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Seed { Quantity = 2 },
                    new Shovel { Quantity = 1 }
                };
            }
            set { }
        }

    }

    public class Grass : LegacyProduct
    {
        protected Grass()
        {
        }

        public Grass(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 30; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.GardeningSuppliesStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Grass; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Seed { Quantity = 1 },
                    new Shovel { Quantity = 1 }
                };
            }
            set { }
        }

    }

    public class Table : LegacyProduct
    {
        protected Table()
        {
        }

        public Table(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 30; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.FurnitureStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Table; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Plank  { Quantity = 1 },
                    new Nail { Quantity = 2 },
                    new Hammer { Quantity = 1 }
                };
            }
            set { }
        }

    }

    public class Glue : LegacyProduct
    {
        public Glue()
        {
        }

        public Glue(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 60; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.BuildingSuppliesStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Glue; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Plastic { Quantity = 1 },
                    new Chemical  { Quantity = 2 }
                };
            }
            set { }
        }

    }

    public class Cement : LegacyProduct
    {
        protected Cement()
        {
        }

        public Cement(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 50; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.BuildingSuppliesStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Cement; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Mineral { Quantity = 2 },
                    new Chemical  { Quantity = 1 }
                };
            }
            set { }
        }
    }

    public class Brick : LegacyProduct
    {
        protected Brick()
        {
        }

        public Brick(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            

        }
        public override int? TimeToFulfill
        {
            get { return 20; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.BuildingSuppliesStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Brick; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Mineral { Quantity = 2 }
                };
            }
            set { }
        }

    }

    public class KitchenUtensils : LegacyProduct
    {
        protected KitchenUtensils()
        {
        }

        public KitchenUtensils(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 45; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.HardwareStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.KitchenUtensils; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Metal { Quantity = 2 },
                    new Wood { Quantity = 2 },
                    new Plastic { Quantity = 2 }
                };
            }
            set { }
        }
    }

    public class Shovel : LegacyProduct
    {
        public Shovel()
        {
        }

        public Shovel(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 30; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.HardwareStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Shovel; }
            set { }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Metal { Quantity = 1 },
                    new Wood { Quantity = 1 },
                    new Plastic { Quantity = 1 },
                };
            }
            set { }
        }

    }

    public class MeasuringTape : LegacyProduct
    {
        public MeasuringTape()
        {
        }

        public MeasuringTape(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 20; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.HardwareStore; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.MeasuringTape; }
            set { }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Metal() { Quantity = 1 }, 
                    new Plastic() { Quantity = 1 }, 
                };
            }
            set {  }
        }
    }

    public class AnimalFeed : LegacyProduct
    {
        public AnimalFeed()
        {
        }

        public AnimalFeed(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 360; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.AnimalFeed; }
            set { }
        }

    }

    public class Glass : LegacyProduct
    {
        public Glass()
        {
        }

        public Glass(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 300; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Glass; }
            set { }
        }

    }

    public class SugarAndSpice : LegacyProduct
    {
        public SugarAndSpice()
        {
        }

        public SugarAndSpice(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 240; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.SugarAndSpice; }
            set { }
        }

    }

    public class Textiles : LegacyProduct
    {
        public Textiles()
        {
        }

        public Textiles(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 180; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Textiles; }
            set { }
        }

    }

    public class Chemical : LegacyProduct
    {
        public Chemical()
        {
        }

        public Chemical(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override string Name
        {
            get { return "Chemical"; }
            set { base.Name = value; }
        }

        public override int? TimeToFulfill
        {
            get { return 120; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
            set { }
        }


        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Chemical; }
            set { }
        }

    }

    public class Mineral : LegacyProduct
    {
        public Mineral()
        {
        }

        public Mineral(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override string Name
        {
            get { return "Mineral"; }
            set { base.Name = value; }
        }

        public override int? TimeToFulfill
        {
            get { return 30; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Mineral; }
            set { }
        }

    }

    public class GreenSmoothie : LegacyProduct
    {
        protected GreenSmoothie()
        {
        }

        public GreenSmoothie(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }
        public override int? TimeToFulfill
        {
            get { return 30; }
            set { }
        }

        public override string Name
        {
            get { return "Green Smoothie"; }
            set {  }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.DonutShop; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.GreenSmoothie; }
            set { }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get { 
                return new LegacyProduct[]
                {
                    new Vegetable{ Quantity = 1 }, 
                    new FruitsAndBerries { Quantity = 1 }
                }; 
            }
            set { base.PreRequisites = value; }
        }
    }

    public class Plastic : LegacyProduct
    {
        public Plastic()
        {

        }

        public Plastic(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override string Name
        {
            get { return "Plastic"; }
            set { base.Name = value; }
        }


        public override int? TimeToFulfill
        {
            get { return 9; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
            set { }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Plastic; }
            set { }
        }

    }

    public class Wood : LegacyProduct
    {
        public Wood()
        {
        }

        public Wood(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }


        public override string Name
        {
            get { return "Wood"; }
            set { base.Name = value; }
        }


        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Wood; }
            set {  }
        }

        public override int? TimeToFulfill
        {
            get { return 3; }
            set { }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
            set { }
        }
    }
}