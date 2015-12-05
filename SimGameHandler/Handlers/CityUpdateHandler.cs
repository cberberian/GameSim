using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using cb.core.data;
using SimGame.Data;
using SimGame.Data.Interface;
using SimGame.Domain;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;
using BuildingUpgrade = SimGame.Domain.BuildingUpgrade;
using Product = SimGame.Domain.Product;

namespace SimGame.Handler.Handlers
{
    public class CityUpdateHandler : ICityUpdateHandler
    {
        private readonly ICityUpdateUnitOfWork _cityUpdateUnitOfWork;

        public CityUpdateHandler(ICityUpdateUnitOfWork cityUpdateUnitOfWork)
        {
            _cityUpdateUnitOfWork = cityUpdateUnitOfWork;
        }

        public CityUpdateResponse UpdateCity(CityUpdateRequest request)
        {
            if (request.City.CurrentCityStorage != null)
                UpdateCityStorage(request.City.CurrentCityStorage);
            var repoRequest = new RepositoryRequest<BuildingUpgrade>
            {
                Expression = GetBuildingUpgradeExpression()
            };
            var result = _cityUpdateUnitOfWork.BuildingUpgradeRepository.Get(repoRequest);
            foreach (var dUpgrade in result)
            {
                if (request.City.BuildingUpgrades.Any(x=>x.Id == dUpgrade.Id))
                    continue;
                dUpgrade.Completed = true;
            }
            foreach (var upgrade in request.City.BuildingUpgrades)
            {
                
                var domainUpgrade = result.FirstOrDefault(x=> x.Id==upgrade.Id);

                if (domainUpgrade == null)
                {
                    domainUpgrade = Mapper.Map<BuildingUpgrade>(upgrade);
                    _cityUpdateUnitOfWork.BuildingUpgradeRepository.Add(domainUpgrade);
                }
                else
                {
                    var changedUpgrade = Mapper.Map<BuildingUpgrade>(upgrade);
                    _cityUpdateUnitOfWork.EntityContext.SetValues(domainUpgrade, changedUpgrade);
                }

                if (domainUpgrade.Products == null)
                    domainUpgrade.Products = new List<Product>();
                DataCollectionMapper.MapCollection(upgrade.Products.Select(Mapper.Map<Product>).ToList(), domainUpgrade.Products, new CollectionMapperOptions
                {
                    Context = _cityUpdateUnitOfWork.EntityContext
                });
            }

            _cityUpdateUnitOfWork.Commit();

            return new CityUpdateResponse();
        }

        private Expression<Func<BuildingUpgrade, bool>> GetBuildingUpgradeExpression()
        {
            return x => x.Completed == false;
        }

        private void UpdateCityStorage(CityStorage currentCityStorage)
        {
            var reporequest = new RepositoryRequest<Product>
            {
                Expression = GetCityStorageExpression()
            };
            var storageProductsResponse = _cityUpdateUnitOfWork
                                                        .ProductRepository
                                                        .Get(reporequest);
            foreach (var product in currentCityStorage.CurrentInventory)
            {
                var cityStorageProduct = storageProductsResponse
                                            .FirstOrDefault(x=>x.ProductTypeId == product.ProductTypeId);
                if (cityStorageProduct == null)
                {
                    cityStorageProduct = new Product
                    {
                        // ReSharper disable once PossibleInvalidOperationException
                        ProductTypeId = product.ProductTypeId.Value,
                        IsCityStorage = true
                    };
                    _cityUpdateUnitOfWork.ProductRepository.Add(cityStorageProduct);
                }
                cityStorageProduct.Quantity = product.Quantity ?? 0;
            }
        }

        private Expression<Func<Product, bool>> GetCityStorageExpression()
        {
            return x => x.IsCityStorage;
        }
    }
}