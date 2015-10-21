using System;
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
            var currentInventory = _db.Products.Where(x=>x.IsCityStorage)
                .OrderBy(y=>y.ProductType.ManufacturerTypeId)
                .ThenBy(z=>z.ProductTypeId).AsEnumerable()
                .ToArray()
                .Select(Mapper.Map<Product>)
                .ToArray();

            var city = new City
            {
                CurrentCityStorage = new CityStorage
                {
                    CurrentInventory = currentInventory
                },
                BuildingUpgrades = _db.BuildingUpgrades.Where(x=>!x.Completed)
                    .OrderBy(y=>y.Name)
                    .ToArray()
                    .Select(Mapper.Map<BuildingUpgrade>)
                    .ToArray()
            };
            var request = new BuildingUpgradeHandlerRequest
            {
                CalculateBuidingUpgradeStatistics = true,
                ReturnFaciltiyAssignment = false,
                ReturnInventory = true,
                City = Mapper.Map<HandlerEntities.City>(city)
            };
            var ret = _buildingUpgradeHandler.CalculateBuildQueue(request);
            
            city.CurrentCityStorage = Mapper.Map<CityStorage>(ret.CityStorage);
            city.BuildingUpgrades =
                ret.OrderedUpgrades.Select(Mapper.Map<BuildingUpgrade>).OrderBy(x => x.Name).ToArray();
            city.RequiredProducts = ret.RequiredProductQueue.Select(Mapper.Map<Product>).ToArray();
            city.AvailableStorage = ret.AvailableStorage.Select(Mapper.Map<Product>).ToArray();
//            city.PendingInventory = ret.PendingInventory.Select(Mapper.Map<Product>).ToArray();
            city.TotalProductsRequired = ret.TotalProductQueue.Select(Mapper.Map<Product>).ToArray();
            city.Manufacturers = _db.Manufacturers.ToArray().Select(Mapper.Map<Manufacturer>).ToArray();
            _buildingUiInfoUpdater.Update(_db.ProductTypes.ToArray(), city);
            return LogHelper.EndLog(logStart, city);
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
            var request = new HandlerEntities.CityUpdateRequest
            {
                City = Mapper.Map<SimGame.Handler.Entities.City>(value)
            };
            _cityUpdateHandler.UpdateCity(request);
            LogHelper.EndLog(ret);
            var city = Get();
            foreach (var bu in city.BuildingUpgrades)
            {
                var firstOrDefault = value.BuildingUpgrades.FirstOrDefault(x=>x.Id == bu.Id);
                bu.CalculateInBuildingUpgrades = firstOrDefault == null || firstOrDefault.CalculateInBuildingUpgrades;
            }
            return LogHelper.EndLog(ret, city);
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
