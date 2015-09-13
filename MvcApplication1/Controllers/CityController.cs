using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using MvcApplication1.Interfaces;
using SimGame.Data;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;
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

        public CityController(ICityUpdateHandler cityUpdateHandler, IBuildingUpgradeHandler buildingUpgradeHandler, IBuildingUiInfoUpdater buildingUiInfoUpdater)
        {
            _cityUpdateHandler = cityUpdateHandler;
            _buildingUpgradeHandler = buildingUpgradeHandler;
            _buildingUiInfoUpdater = buildingUiInfoUpdater;
        }

        // GET api/city
        public City Get()
        {
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
            _buildingUiInfoUpdater.Update(city.BuildingUpgrades, city.CurrentCityStorage, _db.ProductTypes.ToArray());
            return city;
        }

        // GET api/city/5
        public City Get(int id)
        {
            return new City();
        }

        // POST api/city
        public City Post([FromBody]City value)
        {
            if (value==null)
                throw new ApplicationException("Null Data passed to save city.");
            var request = new HandlerEntities.CityUpdateRequest
            {
                City = Mapper.Map<SimGame.Handler.Entities.City>(value)
            };
            _cityUpdateHandler.UpdateCity(request);
            return Get();
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
