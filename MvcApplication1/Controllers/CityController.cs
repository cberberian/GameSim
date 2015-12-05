using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using cb.core;
using log4net;
using SimGame.Data;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;
using SimGame.WebApi.Interfaces;
using BuildingUpgrade = SimGame.WebApi.Models.BuildingUpgrade;
using City = SimGame.WebApi.Models.City;
using CityStorage = SimGame.WebApi.Models.CityStorage;
using HandlerEntities = SimGame.Handler.Entities;
using Manufacturer = SimGame.WebApi.Models.Manufacturer;
using Product = SimGame.WebApi.Models.Product;

namespace SimGame.WebApi.Controllers
{
    public class CityController : ApiController
    {
        private readonly ICityUpdateHandler _cityUpdateHandler;
        private readonly GameSimContext _db = new GameSimContext();
        private readonly IBuildingUpgradeHandler _buildingUpgradeHandler;
        private readonly IBuildingUiInfoUpdater _buildingUiInfoUpdater;
        private ILog _logger;

        private ILog Logger
        {
            get
            {
                if (_logger == null)
                    _logger = LogManager.GetLogger(GetType());
                return _logger;
            }
        }
        public CityController(ICityUpdateHandler cityUpdateHandler, IBuildingUpgradeHandler buildingUpgradeHandler, IBuildingUiInfoUpdater buildingUiInfoUpdater)
        {
            _cityUpdateHandler = cityUpdateHandler;
            _buildingUpgradeHandler = buildingUpgradeHandler;
            _buildingUiInfoUpdater = buildingUiInfoUpdater;
        }

        // GET api/city
        public City Get()
        {
            var logStart = LogHelper.StartLog("Started BuildingUpgradeController.Get", Logger);

            var city = GetCity();
            var ret = _buildingUpgradeHandler.CalculateBuildQueue(new BuildingUpgradeHandlerRequest
            {
                CalculateBuidingUpgradeStatistics = true,
                ReturnFaciltiyAssignment = false,
                ReturnInventory = true,
                City = Mapper.Map<HandlerEntities.City>(city)
            });
            
            city.CurrentCityStorage = Mapper.Map<CityStorage>(ret.CityStorage);
            city.BuildingUpgrades = ret.OrderedUpgrades.Select(Mapper.Map<BuildingUpgrade>).OrderBy(x => x.Name).ToArray();
            city.RequiredProducts = ret.RequiredProductQueue.Select(Mapper.Map<Product>).ToArray();
            city.AvailableStorage = ret.AvailableStorage.Select(Mapper.Map<Product>).ToArray();
            city.TotalProductsRequired = ret.TotalProductQueue.Select(Mapper.Map<Product>).ToArray();
            _buildingUiInfoUpdater.Update(_db.ProductTypes.ToArray(), city);
            return LogHelper.EndLog(logStart, city);
        }

        private City GetCity()
        {
            return new City
            {
                CurrentCityStorage = new CityStorage
                {
                    CurrentInventory = GetCurrentInventory()
                },
                BuildingUpgrades = GetBuildingUpgrades(),
                Manufacturers = GetManufacturers()
            };
        }

        private Manufacturer[] GetManufacturers()
        {
            return _db.Manufacturers
                .Include(x => x.ManufacturerType)
                .Include(x => x.ManufacturingQueueSlots)
                .ToArray().Select(Mapper.Map<Manufacturer>).ToArray();
        }

        private BuildingUpgrade[] GetBuildingUpgrades()
        {
            return _db.BuildingUpgrades
                .Where(x=>!x.Completed)
                .Include(x=>x.Products)
                .OrderBy(y=>y.Name)
                .ToArray()
                .Select(Mapper.Map<BuildingUpgrade>)
                .ToArray();
        }

        private Product[] GetCurrentInventory()
        {
            return _db.Products
                .Where(x=>x.IsCityStorage)
                .Include(x=>x.ProductType.ManufacturerType)
                .OrderBy(y=>y.ProductType.ManufacturerTypeId)
                .ThenBy(z=>z.ProductTypeId).AsEnumerable()
                .ToArray()
                .Select(Mapper.Map<Product>)
                .ToArray();
        }

        // GET api/city/5
        public City Get(int id)
        {
            return new City();
        }

        // POST api/city
        public City Post([FromBody]City value)
        {
            var ret = LogHelper.StartLog("CityController Post", Logger);
            if (value==null)
                throw new ApplicationException("Null Data passed to save city.");
            var mappedCity = Mapper.Map<Handler.Entities.City>(value);
            var request = new HandlerEntities.CityUpdateRequest
            {
                City = mappedCity
            };
            _cityUpdateHandler.UpdateCity(request);
            LogHelper.EndLog(ret);
            var city = Get();

            
            UpdateIncludeInBuildingUpgradesFlag(value, city);
            CalculateBuildQueue(mappedCity, city);

            return LogHelper.EndLog(ret, city);
        }

        private void CalculateBuildQueue(HandlerEntities.City mappedCity, City city)
        {
            var calcRequest = new BuildingUpgradeHandlerRequest
            {
                City = mappedCity,
                CalculateBuidingUpgradeStatistics = true,
                ReturnInventory = true,
                ReturnFaciltiyAssignment = false
            };
            var calculateResults = _buildingUpgradeHandler.CalculateBuildQueue(calcRequest);
            city.RequiredProducts = calculateResults.RequiredProductQueue.Select(Mapper.Map<Product>).ToArray();
            city.RequiredProductsInCityStorage =
                calculateResults.RequiredProductsInCityStorageQueue.Select(Mapper.Map<Product>).ToArray();
            city.TotalProducts = calculateResults.TotalProductQueue.Select(Mapper.Map<Product>).ToArray();
            city.AvailableStorage = calculateResults.AvailableStorage.Select(Mapper.Map<Product>).ToArray();
        }

        private static void UpdateIncludeInBuildingUpgradesFlag(City value, City city)
        {
            foreach (var bu in city.BuildingUpgrades)
            {
                var firstOrDefault = value.BuildingUpgrades.FirstOrDefault(x => x.Id == bu.Id);
                bu.CalculateInBuildingUpgrades = firstOrDefault == null || firstOrDefault.CalculateInBuildingUpgrades;
            }
        }

        // PUT api/city/5
        public void Put(int id, [FromBody]City value)
        {
        }
          
        // DELETE api/city/5
        public void Delete(int id)
        {
        }
    }
}
