using SimGame.Domain;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class Metal : LegacyProduct
    {
        public Metal()
        {
        }

        public Metal(IProduct legacyProduct, InventoryItemResolverOptions options) : base(legacyProduct, options)
        {
            
        }

        public override string Name
        {
            get { return "Metal"; }
            set { base.Name = value; }
        }

        public override int? TimeToFulfill
        {
            get { return 1; }
        }

        public override int? ProductTypeId
        {
            get { return (int)ProductTypeEnum.Metal; }
        }

        public override int FacilityType
        {
            get { return BuildingFacilityType.Factory; }
        }
    }
}