using System.Linq;
using System.Web.Http;
using AutoMapper;
using MvcApplication1.Models;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Handlers;
using City = MvcApplication1.Models.City;


namespace MvcApplication1.Controllers
{
    public class SupplyChainController : ApiController
    {
        private readonly PropertyUpgradeHandler _propertyUpgradeHandler;

        public SupplyChainController(PropertyUpgradeHandler propertyUpgradeHandler)
        {
            _propertyUpgradeHandler = propertyUpgradeHandler;
        }


        // GET api/SupplyChain/5
        public SupplyChain Get(int id)
        {
            return new SupplyChain();
        }

        // GET api/SupplyChain/5
        [HttpPost]
        public SupplyChain Get(SupplyChainRequest request)
        {
            var currentCity = GetCurrentCity(request);
            var calcRequest = new PropertyUpgradeHandlerRequest
            {
                ReturnInventory = true,
                ReturnFaciltiyAssignment = request.ReturnFacilityAssignment,
                City = Mapper.Map<SimGame.Handler.Entities.Legacy.City>(currentCity)
            };

            var ret = _propertyUpgradeHandler.CalculateBuildQueue(calcRequest);
            return new SupplyChain
            {
                CurrentCityStorage = currentCity.CurrentCityStorage,
                RequiredProducts = ret.RequiredProductQueue.Select(Mapper.Map<Product>).ToArray(),
                TotalProducts = ret.TotalProductQueue.Select(Mapper.Map<Product>).ToArray(),
                AvailableStorage = ret.AvailableStorage.Select(Mapper.Map<Product>).ToArray(),

            };
        }

        private static City GetCurrentCity(SupplyChainRequest request)
        {
            var cityStorageUpgrade = request.BuildingUpgrades.FirstOrDefault(x => x.Name.Equals("City Storage"));
            var currentCityStorage = new CityStorage();
            if (cityStorageUpgrade != null && cityStorageUpgrade.Products != null)
            {
                currentCityStorage.CurrentInventory = cityStorageUpgrade.Products.ToArray();
            }
            return new City
            {
                Manufacturers = new Manufacturer[0],
                BuildingUpgrades = request.BuildingUpgrades.Where(x => x.Name != "City Storage").Select(Mapper.Map<BuildingUpgrade>).ToArray(),
                CurrentCityStorage = currentCityStorage
            };
        }
    }
}