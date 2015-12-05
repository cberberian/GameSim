using System.Collections.Generic;
using SimGame.Domain;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class Nail : LegacyProduct
    {
        public Nail()
        {
        }

        public Nail(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 5; }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Nail; }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.BuildingSuppliesStore; }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get { 
                return new LegacyProduct[]
                {
                    new Metal { Quantity = 2 } 
                };
            }
            set {  }
        }
    }
}