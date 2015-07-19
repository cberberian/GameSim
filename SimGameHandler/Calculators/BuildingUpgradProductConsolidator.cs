using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
        private readonly IInventoryFlattener _inventoryFlattener;

        public BuildingUpgradProductConsolidator(IPropertyUpgradeUoW propertyUpgradeUoW, IInventoryFlattener inventoryFlattener)
        {
            _propertyUpgradeUoW = propertyUpgradeUoW;
            _inventoryFlattener = inventoryFlattener;
        }

        public BuildingUpgradeProductConsolidatorResponse GetConsolidatedProductQueue(BuildingUpgradeProductConsoldatorRequest request)
        {
            var combinedProductList = new List<Product>();
            if (request==null || request.BuildingUpgrades == null)
                return new BuildingUpgradeProductConsolidatorResponse
                {
                    ConsolidatedRequiredProductQueue = new Queue<Product>(new Product[]{})
                };

            SetupProductTypes(request);

            //first flatten the list including the dependencies.
            var flattenerResponse = GetFlattenedInventory(request);
            if (!flattenerResponse.Products.Any())
                return new BuildingUpgradeProductConsolidatorResponse
                {
                    ConsolidatedRequiredProductQueue = new Queue<Product>(),
                    ConsolidatedTotalProductQueue = new Queue<Product>(),
                    AvailableStorage = new List<Product>()
                };

            //now combine the redundant queued products into one productToRemove per productToRemove type
            foreach (var productType in _productTypes)
            {
                var productTypeId = productType.Id;
                Product combinedItem = null;
                var itemAdded = false;
                var queuedProducts = flattenerResponse.Products.Where(x => x.ProductTypeId.HasValue && x.ProductTypeId.Value == productTypeId).ToArray();
                
                foreach (var queuedProduct in queuedProducts)
                {
                    if (combinedItem == null)
                        combinedItem = new Product(productType);


                    var newItemsQuantity = (queuedProduct.Quantity ?? 0);
                    var totalCurrentQuantity = combinedItem.Quantity;
                    var newTotalQuantity = newItemsQuantity + totalCurrentQuantity;
                    var quantityToAdd = newTotalQuantity;
                    if (quantityToAdd <= 0)
                        break; //done with productToRemove type because we have the rest in storage.
                    
                    combinedItem.Quantity = quantityToAdd;

                    if (combinedItem.ProductType.ManufacturerType.SupportsParallelManufacturing)
                    {
                        if (itemAdded) //if not added then we just increment the count.
                            continue;
                        combinedItem.TotalDuration = productType.TimeToManufacture;
                        itemAdded = true;
                    }
                    else
                    {
                        combinedItem.TotalDuration += productType.TimeToManufacture * quantityToAdd;
                    }
                }
                    
                if (combinedItem != null && combinedItem.Quantity > 0)
                    combinedProductList.Add(combinedItem);

            }

            //now remove products we have in storage including the dependents. 
            var cityStorage = request.CityStorage ?? new CityStorage();
            var requiredProductList = combinedProductList.Select(x=>x.Clone()).ToList();
            foreach (var product in cityStorage.CurrentInventory)
            {
                DecreaseProductQuantity(product, ref requiredProductList);
            }

            //Available Storage = availablestorage - whatsneeded


            //return the list in order by manufacturer type id then by how long to fulfill the item. 
            var ret = new BuildingUpgradeProductConsolidatorResponse
            {
                ConsolidatedRequiredProductQueue = new Queue<Product>(requiredProductList.OrderBy(x => x.ManufacturerTypeId).ThenBy(y => y.TimeToFulfill)),
                ConsolidatedTotalProductQueue = new Queue<Product>(combinedProductList.OrderBy(x => x.ManufacturerTypeId).ThenBy(y => y.TimeToFulfill)),
                AvailableStorage = GetAvailableStorageList(cityStorage, combinedProductList)
                
            };
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
//            var changedStorage = new List<Product>();
//            foreach (var combProd in combinedProductList)
//            {
//                var localCombinedProduct = combProd;
//                var clone = cityStorage.CurrentInventory.First(x => x.ProductTypeId == localCombinedProduct.ProductTypeId).Clone();
//                var newQuantity = clone.Quantity - localCombinedProduct.Quantity;
//                clone.Quantity = newQuantity < 0 ? 0 : newQuantity;
//                changedStorage.Add(clone);
//            }
            return ret; 
        }

        /// <summary>
        /// Decreases the quanity of the list product. if the quanity is drained then remove the list product 
        /// </summary>
        /// <param name="productToRemove"></param>
        /// <param name="listToDescreaseQuantity"></param>
        private void DecreaseProductQuantity(Product productToRemove, ref List<Product> listToDescreaseQuantity)
        {
            if (!productToRemove.Quantity.HasValue || productToRemove.Quantity < 1)
                return;
            var productToDecreaseQuantity = listToDescreaseQuantity.FirstOrDefault(x => x.ProductTypeId == productToRemove.ProductTypeId);
            if (productToDecreaseQuantity == null) return;
            
            productToDecreaseQuantity.Quantity = productToDecreaseQuantity.Quantity - productToRemove.Quantity;
            if (productToDecreaseQuantity.Quantity < 1)
                listToDescreaseQuantity.Remove(productToDecreaseQuantity);
            
            var productTypeBeingRemoved = _productTypes.FirstOrDefault(x => x.Id == productToDecreaseQuantity.ProductTypeId);
            
            if (productTypeBeingRemoved == null) return;
            //remove required products for each item in the product to remove. 
            for (var a = 0; a < productToRemove.Quantity; a++)
            foreach (var product in productTypeBeingRemoved.RequiredProducts)
            {
                DecreaseProductQuantity(product, ref listToDescreaseQuantity);
            }
        }

        private InventoryFlattenerResponse GetFlattenedInventory(BuildingUpgradeProductConsoldatorRequest request)
        {
            var inventoryFlattenerRequest = new InventoryFlattenerRequest
            {
                BuildingUpgrades = request.BuildingUpgrades,
                ProductTypes = _productTypes
            };
            return _inventoryFlattener.GetFlattenedInventory(inventoryFlattenerRequest);
        }

        private void SetupProductTypes(BuildingUpgradeProductConsoldatorRequest request)
        {
            if (request.ProductTypes == null)
                request.ProductTypes = _propertyUpgradeUoW.ProductTypeRepository.Get().Select(Mapper.Map<ProductType>).ToArray();
            _productTypes = request.ProductTypes;
        }

        private static int GetTotalInStorage(ProductType productType, CityStorage cityStorage)
        {
            var product = cityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == productType.Id);
            return product == null ? 0 : product.Quantity ?? 0;
        }
    }
}