using System.Collections.Generic;

namespace SimGame.Handler.Entities.Legacy
{
    public class BuildingFacility : BuildingFacilityType
    {
        protected BuildingFacility()
        {
            ManufacturingQueue = new Queue<LegacyProduct>();
        }
        public Queue<LegacyProduct> ManufacturingQueue { get; set; }
    }
}