using System.Collections.Generic;
using System.Linq;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Calculators
{
    public class InventoryFlattener : IInventoryFlattener
    {
        private ProductType[] _productTypes;
        private CityStorage _cityStorage;

        public InventoryFlattenerResponse GetFlattenedInventory(InventoryFlattenerRequest inventoryFlattenerRequest)
        {
            _productTypes = inventoryFlattenerRequest.ProductTypes;
            _cityStorage = inventoryFlattenerRequest.CityStorage == null ? new CityStorage
            {
                CurrentInventory = new Product[0]
            } : inventoryFlattenerRequest.CityStorage.Clone();
//            var flattenedList = inventoryFlattenerRequest.Products.SelectMany(upgrade => GetFlattenedInventory(upgrade.Products)).ToArray();
            var flattenedList = GetFlattenedInventory(inventoryFlattenerRequest.Products);
            return new InventoryFlattenerResponse
            {
                Products = flattenedList
            };
        }

        private Product[] GetFlattenedInventory(IEnumerable<Product> requriedInventoryItems)
        {
            var ret = new List<Product>();
            if (requriedInventoryItems == null)
                return new Product[0];

            foreach (var exitingItem in requriedInventoryItems)
            {
                var item = exitingItem.Clone();
                item.Quantity = GetQuantityToManufacture(item, _cityStorage);
                item.TimeToFulfill = 0;
                item.TimeToFulfillPrerequisites = 0;
                item.TotalDuration = 0;
                var productType = _productTypes.First(x => x.Id == item.ProductTypeId);
                item.Name = productType.Name;
                
                if (item.Quantity == 0)
                    continue;
                for (var a = 0; a < item.Quantity; a++)
                {
                    var requiredProducts = GetFlattenedInventory(productType.RequiredProducts);
                    item.TimeToFulfill += productType.TimeToManufacture ?? 0;
                    item.TimeToFulfillPrerequisites += requiredProducts.Sum(x => x.TotalDuration ?? 0);
                    ret.AddRange(requiredProducts);
                }
                item.TotalDuration = item.TimeToFulfill + item.TimeToFulfillPrerequisites;
                ret.Add(item);
            }
            return  ret.GroupBy(x => x.ProductTypeId)
                    .Select(grping => new Product
                    {
                        Quantity = grping.Sum(qty=>qty.Quantity),
                        TimeToFulfill = grping.Sum(ttf=>ttf.TimeToFulfill),
                        TimeToFulfillPrerequisites = grping.Sum(ttfp=>ttfp.TimeToFulfillPrerequisites),
                        TotalDuration = grping.Sum(ttm=>ttm.TotalDuration),
                        ProductTypeId = grping.Key,
                        Name = grping.First().Name
                    }).OrderBy(x => x.ManufacturerTypeId).ThenBy(y => y.TotalDuration)
                    .ToArray();
        }

        private static int? GetQuantityToManufacture(Product item, CityStorage storage)
        {
            var storageProduct = storage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == item.ProductTypeId);
            if (storageProduct == null)
                return item.Quantity;
            var newStorage = storageProduct.Quantity - item.Quantity;
            if (newStorage <= 0)
            {
                storageProduct.Quantity = 0;
                return -1 * newStorage;

            }
            storageProduct.Quantity = newStorage;
            return 0;
        }
    }
}