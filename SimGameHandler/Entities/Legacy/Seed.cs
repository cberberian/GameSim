using SimGame.Domain;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class Seed : LegacyProduct
    {
        public Seed()
        {
        }

        public Seed(IProduct legacyProduct, InventoryItemResolverOptions options)
            : base(legacyProduct, options)
        {
            
        }

        public override string Name
        {
            get { return "Seed"; }
            set { base.Name = value; }
        }

        public override int? TimeToFulfill
        {
            get { return 20; }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Seed; }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
        }

        
    }
}