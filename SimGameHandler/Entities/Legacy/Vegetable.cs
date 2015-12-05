using System.Collections.Generic;
using SimGame.Domain;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class Vegetable : LegacyProduct
    {
        public Vegetable()
        {
        }

        public Vegetable(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 20; }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Vegetable; }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.FarmersMarket; }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Seed { Quantity = 2 }
                };
            }
            set { }
        }
    }
}