using System.Collections.Generic;
using SimGame.Domain;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class Hammer : LegacyProduct
    {
        public Hammer()
        {
        }

        public Hammer(IProduct legacyProduct, InventoryItemResolverOptions options) : base(legacyProduct, options)
        {
            
        }

        public override int? TimeToFulfill
        {
            get { return 14; }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Hammer; }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.HardwareStore; }
        }

        public override IEnumerable<IProduct> PreRequisites
        {
            get
            {
                return new LegacyProduct[]
                {
                    new Metal { Quantity = 1 },
                    new Wood { Quantity = 1 }
                };
            }
            set {  }
        }
    }
}