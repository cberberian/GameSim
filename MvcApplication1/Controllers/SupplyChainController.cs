using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using cb.core;
using log4net;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;
using SimGame.WebApi.Interfaces;
using SimGame.WebApi.Models;
using BuildingUpgrade = SimGame.WebApi.Models.BuildingUpgrade;
using City = SimGame.WebApi.Models.City;
using CityStorage = SimGame.WebApi.Models.CityStorage;
using Manufacturer = SimGame.WebApi.Models.Manufacturer;
using Product = SimGame.WebApi.Models.Product;
using ProductType = SimGame.Domain.ProductType;

namespace SimGame.WebApi.Controllers
{
    public class SupplyChainController : ApiController
    {
        private readonly IBuildingUpgradeHandler _buldingUpgradeHandler;
        private readonly ICityStorageCalculator _cityStorageCalculator;
        private readonly IGameSimContext _dbContext;
        private readonly IBuildingUiInfoUpdater _buildingUiInfoUpdater;
        private ILog _logger;

        private ILog Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger(GetType())); }
        }
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
            var logStart = LogHelper.StartLog("Begin Supply Chain Get", Logger);
            //Get the current city from the database. 
            var currentCity = ConvertRequestToHandlerCity(request);
            var prodTypes = _dbContext.ProductTypes
                                    .AsQueryable()
                                    .Include(x=>x.RequiredProducts)
                                    .Include(y=>y.ManufacturerType.ProductTypes)
                                    .ToArray();

            var calcRequest = new BuildingUpgradeHandlerRequest
            {
                ReturnInventory = true,
                ReturnFaciltiyAssignment = request.ReturnFacilityAssignment,
                City = Mapper.Map<Handler.Entities.City>(currentCity),
                ProductTypes = prodTypes
            };
            var strgResults = CalculateNewCityStorageAmounts(request, calcRequest, prodTypes);
            
            var ret = _buldingUpgradeHandler.CalculateBuildQueue(calcRequest);

            var city = new City
            {
                CurrentCityStorage = Mapper.Map<CityStorage>(strgResults.CityStorage),
                RequiredProducts = ret.RequiredProductQueue.Select(Mapper.Map<Product>).ToArray(),
                RequiredProductsInCityStorage = ret.RequiredProductsInCityStorageQueue.Select(Mapper.Map<Product>).ToArray(),
                TotalProducts = ret.TotalProductQueue.Select(Mapper.Map<Product>).ToArray(),
                AvailableStorage = ret.AvailableStorage.Select(Mapper.Map<Product>).ToArray(),
                BuildingUpgrades = ret.OrderedUpgrades.Select(Mapper.Map<BuildingUpgrade>).ToArray().OrderBy(x=>x.Name).ToArray()
            };
            UpdateBuildingUpgradeUiInfo(prodTypes, city);
            return LogHelper.EndLog(logStart, city);

        }

        private void UpdateBuildingUpgradeUiInfo(ProductType[] prodTypes, City city)
        {

            _buildingUiInfoUpdater.Update(prodTypes, city);
            
        }

        private CalculateStorageResponse CalculateNewCityStorageAmounts(SupplyChainRequest request, BuildingUpgradeHandlerRequest calcRequest, ProductType[] productTypes)
        {
            var strgResults = new CalculateStorageResponse
            {
                CityStorage = calcRequest.City.CurrentCityStorage
            };
            //If we have products queued for production then calculate the new city storage.
            if (request.RequiredProductUpdates == null) return strgResults;
            var calculateStorageRequest = new CalculateStorageRequest
            {
                ProductTypes = productTypes,
                NewProductQuantities = request.RequiredProductUpdates.Select(Mapper.Map<Handler.Entities.Product>).ToArray(),
                CityStorage = calcRequest.City.CurrentCityStorage
            };
            return _cityStorageCalculator.CalculateNewStorageAmounts(calculateStorageRequest);
        }

        private static City ConvertRequestToHandlerCity(SupplyChainRequest request)
        {
            var buildingUpgrades = request.BuildingUpgrades ?? new BuildingUpgrade[0];
            var cityStorageUpgrade = buildingUpgrades.FirstOrDefault(x => x.Name.Equals("City Storage"));
            var currentCityStorage = request.CurrentCityStorage ?? new CityStorage
            {
                CurrentInventory = cityStorageUpgrade == null ? new Product[0] : cityStorageUpgrade.Products
            };

            return new City
            {
                Manufacturers = new Manufacturer[0],
                BuildingUpgrades = buildingUpgrades.Where(x => x.Name != "City Storage").ToArray(),
                CurrentCityStorage = currentCityStorage
            };
        }
    }
}