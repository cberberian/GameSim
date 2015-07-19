using System.Collections.Generic;
using System.Linq;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Calculators
{
    public class InventoryFlattener : IInventoryFlattener
    {
        private ProductType[] _productTypes;

        public InventoryFlattenerResponse GetFlattenedInventory(InventoryFlattenerRequest inventoryFlattenerRequest)
        {
            _productTypes = inventoryFlattenerRequest.ProductTypes;
            var flattenedList = inventoryFlattenerRequest.BuildingUpgrades.SelectMany(upgrade => GetFlattenedInventory(upgrade.Products)).ToArray();
            return new InventoryFlattenerResponse
            {
                Products = flattenedList
            };
        }

        private IEnumerable<Product> GetFlattenedInventory(IEnumerable<Product> requriedInventoryItems)
        {
            var ret = new List<Product>();
            if (requriedInventoryItems == null)
                return ret;

            foreach (var item in requriedInventoryItems.OrderBy(x => x.ManufacturerTypeId).ThenBy(y => y.TimeToFulfill))
            {
                for (var a = 0; a < item.Quantity; a++)
                {
                    var productType = _productTypes.First(x => x.Id == item.ProductTypeId);
                    ret.AddRange(GetFlattenedInventory(productType.RequiredProducts));
                }
                ret.Add(item);
            }
            return ret;
        }
    }
}