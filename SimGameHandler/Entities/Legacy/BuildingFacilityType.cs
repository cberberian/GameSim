using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Entities.Legacy
{
    public class BuildingFacilityType
    {
        public static int Factory = 1;
        public static int HardwareStore = 2;
        public static int BuildingSuppliesStore = 3;
        public static int FurnitureStore = 4;
        public static int GardeningSuppliesStore = 5;
        public static int FarmersMarket = 6;
        public static int DonutShop = 7;
        public static int FashionStore = 8;

        public static bool SupportsParallelManufacturing(int facilityType)
        {
            return facilityType == Factory;
        }

        public virtual string Name { get; set; }
        public virtual int QueueSize { get; set; }
        public virtual int FacilityType { get; set; }
        public virtual bool ParallelProcessing { get; set; }
        public virtual IProduct[] SupportedItems { get; set; }
        
    }
}