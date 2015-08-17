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
                    ConsolidatedTotalProductQueue = new Queue<Product>(),
                    ConsolidatedRequiredProductQueue = new Queue<Product>(new Product[]{}),
                    ConsolidatedRequiredProductsInStorage = new List<Product>(),
                    AvailableStorage = new List<Product>()
                };

            SetupProductTypes(request);

            //first flatten the list including the dependencies.
            var flattenerResponse = GetFlattenedInventory(request);
            if (!flattenerResponse.Products.Any())
                return new BuildingUpgradeProductConsolidatorResponse
                {
                    ConsolidatedRequiredProductQueue = new Queue<Product>(),
                    ConsolidatedTotalProductQueue = new Queue<Product>(),
                    AvailableStorage = new List<Product>(),
                    ConsolidatedRequiredProductsInStorage = new List<Product>()
                };

            CombineDuplicateProductInstances(flattenerResponse, combinedProductList);

            //now remove products we have in storage including the dependents. 
            var cityStorage = request.CityStorage ?? new CityStorage
            {
                CurrentInventory = new Product[0]
            };
            var requiredProductList = combinedProductList.Select(x=>x.Clone()).ToList();
            foreach (var storageProduct in cityStorage.CurrentInventory)
            {
                DecreaseProductQuantity(storageProduct, ref requiredProductList);
            }

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

        private List<Product> GetRequiredInStorageList(CityStorage cityStorage, List<Product> combinedProductList)
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

        /// <summary>
        /// Takes duplicated products and totals them into 1 product with total quantity. 
        /// </summary>
        /// <param name="flattenerResponse"></param>
        /// <param name="combinedProductList"></param>
        private void CombineDuplicateProductInstances(InventoryFlattenerResponse flattenerResponse, List<Product> combinedProductList)
        {

            foreach (var productType in _productTypes)
            {
                var productTypeId = productType.Id;
                Product combinedItem = null;
                var itemAdded = false;
                var queuedProducts =
                    flattenerResponse.Products.Where(x => x.ProductTypeId.HasValue && x.ProductTypeId.Value == productTypeId)
                        .ToArray();

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
                        combinedItem.TotalDuration += productType.TimeToManufacture*quantityToAdd;
                    }
                }

                if (combinedItem != null && combinedItem.Quantity > 0)
                    combinedProductList.Add(combinedItem);
            }
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
        /// <param name="productInStorage"></param>
        /// <param name="requiredProducts"></param>
        private void DecreaseProductQuantity(Product productInStorage, ref List<Product> requiredProducts)
        {
            //if we don't have in storage then no need to decrement quanities of required list.
            if (ProductIsNotInStorage(productInStorage))
                return;

            var requiredProduct = GetMatchingProductByProductType(productInStorage, requiredProducts);
            //no required products are in the list for this storage product. 
            if (requiredProduct == null) return;
            
            //decrement the required product by the amount in storage.
            requiredProduct.Quantity = requiredProduct.Quantity - productInStorage.Quantity;
            
            //if no required products are now required then remove the product from the list and 
            if (requiredProduct.Quantity < 1)
            {
                requiredProducts.Remove(requiredProduct);
                //no need to process children since we don't need this product anymore.
                return;
            }
            
            //now get the producttype of that 
            var productTypeBeingRemoved = _productTypes.FirstOrDefault(x => x.Id == requiredProduct.ProductTypeId);
            
            if (productTypeBeingRemoved == null) return;
            //for each required product decrement by amount  
            for (var a = 0; a < productInStorage.Quantity; a++)
            {
                foreach (var product in productTypeBeingRemoved.RequiredProducts)
                {
                    DecreaseProductQuantity(product, ref requiredProducts);
                }
            }
        }

        private static Product GetMatchingProductByProductType(Product productInStorage, List<Product> requiredProducts)
        {
            return requiredProducts.FirstOrDefault(x => x.ProductTypeId == productInStorage.ProductTypeId);
        }

        private static bool ProductIsNotInStorage(Product productInStorage)
        {
            return !productInStorage.Quantity.HasValue || productInStorage.Quantity < 1;
        }

        private InventoryFlattenerResponse GetFlattenedInventory(BuildingUpgradeProductConsoldatorRequest request)
        {
            var inventoryFlattenerRequest = new InventoryFlattenerRequest
            {
                Products = request.BuildingUpgrades.SelectMany(x=>x.Products).ToArray(),
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