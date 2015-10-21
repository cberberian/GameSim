using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Calculators
{
    public class BuildingUpgradProductConsolidator : IBuildingUpgradProductConsolidator
    {
        private ProductType[] _productTypes;

        private readonly IPropertyUpgradeUoW _propertyUpgradeUoW;
        private readonly IRequiredProductFlattener _requiredProductFlattener;

        public BuildingUpgradProductConsolidator(IPropertyUpgradeUoW propertyUpgradeUoW, IRequiredProductFlattener requiredProductFlattener)
        {
            _propertyUpgradeUoW = propertyUpgradeUoW;
            _requiredProductFlattener = requiredProductFlattener;
        }

        public BuildingUpgradeProductConsolidatorResponse GetConsolidatedProductQueue(BuildingUpgradeProductConsoldatorRequest request)
        {
            
            if (request==null || request.BuildingUpgrades == null)
                return new BuildingUpgradeProductConsolidatorResponse
                {
                    ConsolidatedTotalProductQueue = new Queue<Product>(),
                    ConsolidatedRequiredProductQueue = new Queue<Product>(new Product[]{}),
                    ConsolidatedRequiredProductsInStorage = new List<Product>(),
                    AvailableStorage = new List<Product>()
                };

            SetupProductTypes(request);

            //get flattened list including the dependencies.
            var combinedProductList = GetFlattenedProductRequirementList(request, null).Products;
            if (!combinedProductList.Any())
                return new BuildingUpgradeProductConsolidatorResponse
                {
                    ConsolidatedRequiredProductQueue = new Queue<Product>(),
                    ConsolidatedTotalProductQueue = new Queue<Product>(),
                    AvailableStorage = new List<Product>(),
                    ConsolidatedRequiredProductsInStorage = new List<Product>()
                };

            //now remove products we have in storage including the dependents. 
            var cityStorage = request.CityStorage ?? new CityStorage
            {
                CurrentInventory = new Product[0]
            };
            // Get Required list by flattening the total with city storage. 
            var requiredProductList = GetFlattenedProductRequirementList(request, cityStorage).Products;

            //return the list in order by manufacturer type id then by how long to fulfill the item. 
            var availableStorageList = GetAvailableStorageList(cityStorage, combinedProductList);
            var requiredInStorage = GetRequiredInStorageList(cityStorage, combinedProductList);

            var ret = new BuildingUpgradeProductConsolidatorResponse
            {
                ConsolidatedRequiredProductQueue = new Queue<Product>(requiredProductList.OrderBy(x => x.ManufacturerTypeId).ThenBy(y => y.TimeToFulfill)),
                ConsolidatedTotalProductQueue = new Queue<Product>(combinedProductList.OrderBy(x => x.ManufacturerTypeId).ThenBy(y => y.TimeToFulfill)),
                AvailableStorage = availableStorageList,
                ConsolidatedRequiredProductsInStorage = requiredInStorage
                
            };
            return ret;
        }

        private List<Product> GetRequiredInStorageList(CityStorage cityStorage, IEnumerable<Product> combinedProductList)
        {
            var ret = new List<Product>();
            foreach (var product in combinedProductList)
            {
                var storedProduct = cityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == product.ProductTypeId);
                if (storedProduct != null && storedProduct.Quantity >= product.Quantity)
                {
                    ret.Add(product);
                }
            }
            return ret; 
        }

        private List<Product> GetAvailableStorageList(CityStorage cityStorage, IEnumerable<Product> combinedProductList)
        {
            var ret = new List<Product>();
            foreach (var item in cityStorage.CurrentInventory)
            {
                var availableProduct = item.Clone();
                if (availableProduct.Quantity > 0)
                {
                    // ReSharper disable once PossibleMultipleEnumeration
                    var requireProduct = combinedProductList.FirstOrDefault(x => x.ProductTypeId == item.ProductTypeId);
                    if (requireProduct != null)
                    {
                        availableProduct.Quantity = availableProduct.Quantity - requireProduct.Quantity;
                        if (availableProduct.Quantity < 0) availableProduct.Quantity = 0;
                    }
                }
                if (availableProduct.Quantity > 0)
                    ret.Add(availableProduct);
            }
            return ret; 
        }

        private RequiredProductFlattenerResponse GetFlattenedProductRequirementList(BuildingUpgradeProductConsoldatorRequest request, CityStorage cityStorage)
        {
            var inventoryFlattenerRequest = new RequiredProductFlattenerRequest
            {
                Products = request.BuildingUpgrades.Where(x=>x.CalculateInBuildingUpgrades).SelectMany(x=>x.Products).ToArray(),
                ProductTypes = _productTypes,
                CityStorage = cityStorage
            };
            return _requiredProductFlattener.GetFlattenedInventory(inventoryFlattenerRequest);
        }

        private void SetupProductTypes(BuildingUpgradeProductConsoldatorRequest request)
        {
            if (request.ProductTypes == null)
                request.ProductTypes = _propertyUpgradeUoW.ProductTypeRepository.Get().Select(Mapper.Map<ProductType>).ToArray();
            _productTypes = request.ProductTypes;
        }
    }
}