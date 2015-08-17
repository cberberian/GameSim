using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Http;
using AutoMapper;
using MvcApplication1.Interfaces;
using MvcApplication1.Models;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;
using BuildingUpgrade = MvcApplication1.Models.BuildingUpgrade;
using City = MvcApplication1.Models.City;
using CityStorage = MvcApplication1.Models.CityStorage;
using Manufacturer = MvcApplication1.Models.Manufacturer;
using Product = MvcApplication1.Models.Product;
using ProductType = SimGame.Domain.ProductType;

namespace MvcApplication1.Controllers
{
    public class SupplyChainController : ApiController
    {
        private readonly IBuildingUpgradeHandler _buldingUpgradeHandler;
        private readonly ICityStorageCalculator _cityStorageCalculator;
        private readonly IGameSimContext _dbContext;
        private readonly IBuildingUiInfoUpdater _buildingUiInfoUpdater;

        public SupplyChainController(IBuildingUpgradeHandler buldingUpgradeHandler, ICityStorageCalculator cityStorageCalculator, IGameSimContext dbContext, IBuildingUiInfoUpdater buildingUiInfoUpdater)
        {
            _buldingUpgradeHandler = buldingUpgradeHandler;
            _cityStorageCalculator = cityStorageCalculator;
            _dbContext = dbContext;
            _buildingUiInfoUpdater = buildingUiInfoUpdater;
        }


        // GET api/SupplyChain/5
        public SupplyChain Get(int id)
        {
            return new SupplyChain();
        }

        // GET api/SupplyChain/5
        [HttpPost]
        public City Get(SupplyChainRequest request)
        {
            //Get the current city from the database. 
            var currentCity = ConvertRequestToHandlerCity(request);
            var prodTypes = _dbContext.ProductTypes.ToArray();
            var calcRequest = new BuildingUpgradeHandlerRequest
            {
                ReturnInventory = true,
                ReturnFaciltiyAssignment = request.ReturnFacilityAssignment,
                City = Mapper.Map<SimGame.Handler.Entities.City>(currentCity)
            };
            var strgResults = CalculateNewCityStorageAmounts(request, calcRequest);
            
            var ret = _buldingUpgradeHandler.CalculateBuildQueue(calcRequest);


            var currentCityStorage = Mapper.Map<CityStorage>(strgResults.CityStorage);
            var requiredProducts = ret.RequiredProductQueue.Select(Mapper.Map<Product>).ToArray();
            var requiredProductsInCityStorage = ret.RequiredProductsInCityStorageQueue.Select(Mapper.Map<Product>).ToArray();
            var totalProducts = ret.TotalProductQueue.Select(Mapper.Map<Product>).ToArray();
            var availableStorage = ret.AvailableStorage.Select(Mapper.Map<Product>).ToArray();
            var buildingUpgrades = ret.OrderedUpgrades.Select(Mapper.Map<BuildingUpgrade>).ToArray();
            UpdateBuildingUpgradeUiInfo(buildingUpgrades, currentCityStorage, prodTypes);
            return new City
            {
                CurrentCityStorage = currentCityStorage,
                RequiredProducts = requiredProducts,
                RequiredProductsInCityStorage = requiredProductsInCityStorage,
                TotalProducts = totalProducts,
                AvailableStorage = availableStorage,
                BuildingUpgrades = buildingUpgrades.OrderBy(x=>x.Name).ToArray()
            };
//            return new SupplyChain
//            {
//                City = city,
//                CurrentCityStorage = currentCityStorage,
//                RequiredProducts = requiredProducts,
//                RequiredProductsInCityStorage = requiredProductsInCityStorage,
//                RequiredProducts = totalProducts,
//                AvailableStorage = availableStorage,
//                BuildingUpgrades = buildingUpgrades
//
//            };
        }

        private void UpdateBuildingUpgradeUiInfo(BuildingUpgrade[] buildingUpgrades, CityStorage currentCityStorage,
            ProductType[] prodTypes)
        {

            _buildingUiInfoUpdater.Update(buildingUpgrades, currentCityStorage, prodTypes);
            
        }

        private CalculateStorageResponse CalculateNewCityStorageAmounts(SupplyChainRequest request,
            BuildingUpgradeHandlerRequest calcRequest)
        {
            var strgResults = new CalculateStorageResponse
            {
                CityStorage = calcRequest.City.CurrentCityStorage
            };
            //If we have products queued for production then calculate the new city storage.
            if (request.RequiredProductUpdates != null)
            {
                var calculateStorageRequest = new CalculateStorageRequest
                {
                    NewProductQuantities = request.RequiredProductUpdates.Select(Mapper.Map<SimGame.Handler.Entities.Product>).ToArray(),
                    CityStorage = calcRequest.City.CurrentCityStorage
                };
                strgResults = _cityStorageCalculator.CalculateNewStorageAmounts(calculateStorageRequest);
            }
            return strgResults;
        }

        private static City ConvertRequestToHandlerCity(SupplyChainRequest request)
        {

            
            var cityStorageUpgrade = request.BuildingUpgrades.FirstOrDefault(x => x.Name.Equals("City Storage"));
            var currentCityStorage = request.CurrentCityStorage ?? new CityStorage
            {
                CurrentInventory = cityStorageUpgrade == null ? new Product[0] : cityStorageUpgrade.Products
            };

            return new City
            {
                Manufacturers = new Manufacturer[0],
                BuildingUpgrades = request.BuildingUpgrades.Where(x => x.Name != "City Storage").ToArray(),
                CurrentCityStorage = currentCityStorage
            };
        }
    }
}