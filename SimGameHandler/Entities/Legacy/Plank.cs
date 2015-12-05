using System.Collections.Generic;
using SimGame.Domain;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class Plank : LegacyProduct
    {
        public Plank()
        {
        }

        public Plank(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 30; }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Planks; }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.BuildingSuppliesStore; }
        }
        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Wood { Quantity = 2 }
                };
            }
            set { }
        }
    }
}