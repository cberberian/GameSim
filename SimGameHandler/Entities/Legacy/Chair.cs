using System.Collections.Generic;
using SimGame.Domain;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class Chair : LegacyProduct
    {
        public Chair()
        {
        }

        public Chair(IProduct legacyProduct, InventoryItemResolverOptions options) : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 20; }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Chair; }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.FurnitureStore; }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Nail { Quantity = 1 },
                    new Wood { Quantity = 2 },
                    new Hammer  { Quantity = 1 }
                };
            }
            set { }
        }
    }
}