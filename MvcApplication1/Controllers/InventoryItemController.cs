using System.Collections.Generic;
using System.Web.Http;
using MvcApplication1.Models;
using SimGame.Domain;
using BuildingFacilityType = SimGame.Handler.Entities.Legacy.BuildingFacilityType;

namespace MvcApplication1.Controllers
{
    public class InventoryItemController : ApiController
    {
        // GET api/InventoryItems
        public IEnumerable<InventoryItem> Get()
        {
            var metal = new InventoryItem
            {
                Id = 1,
                ItemType = ProductTypeEnum.Metal,
                Name = "Metal",
                FacilityType = BuildingFacilityType.Factory,
                DurationInMinutes = 1,
                Prerequisites = new []
                {
                    new InventoryItem
                    {
                        Quantity = 2,
                        Name = "test",
                        FacilityType = BuildingFacilityType.Factory,
                        DurationInMinutes = 10
                    }
                }
                    
            };
            ;
            return new[]
            {
                metal                
            };
        }

        // GET api/InventoryItems/5
        public InventoryItem Get(int id)
        {
            return new InventoryItem();
        }

    }
}
