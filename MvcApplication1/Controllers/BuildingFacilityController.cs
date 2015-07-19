using System.Collections.Generic;
using System.Web.Http;
using MvcApplication1.Models;
using BuildingFacilityType = SimGame.Handler.Entities.Legacy.BuildingFacilityType;
using handler = SimGame.Handler;

namespace MvcApplication1.Controllers
{
    public class BuildingFacilityController : ApiController
    {
        // GET api/InventoryItems
        public IEnumerable<BuildingFacility> Get()
        {
            return new[]
            {
                new BuildingFacility
                {
                    QueueSize = 2,
                    FacilityType = BuildingFacilityType.Factory
                } 
            };
        }

        // GET api/InventoryItems/5
        public BuildingFacility Get(int id)
        {
            return new BuildingFacility();
        }
    }

}
