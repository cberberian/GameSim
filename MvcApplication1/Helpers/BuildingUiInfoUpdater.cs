using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SimGame.WebApi.Interfaces;
using SimGame.WebApi.Models;
using ProductType = SimGame.Domain.ProductType;

namespace SimGame.WebApi.Helpers
{
    public class BuildingUiInfoUpdater : IBuildingUiInfoUpdater
    {
        public void Update(ProductType[] prodTypes, City city)
        {
            foreach (var prod in city.CurrentCityStorage.CurrentInventory)
            {
//                foreach (var prod2 in city.RequiredProducts)
//                    prod2.StorageQuantity = storageProduct.Quantity;
                UpdateProductStorageQuantity(city.RequiredProducts, prod);
                UpdateProductStorageQuantity(city.AvailableStorage, prod);
                UpdateProductStorageQuantity(city.TotalProductsRequired, prod);
            }

            UpdateKeywords(city.RequiredProducts, prodTypes, city.BuildingUpgrades, null);

            foreach (var bu in city.BuildingUpgrades)
            {
                UpdateKeywords(bu.Products, prodTypes, null, bu);
                var buildingUpgradeTooltip = new StringBuilder();
                buildingUpgradeTooltip.Append("<div>");
                //add total hours remaining
                buildingUpgradeTooltip.AppendFormat("{0}:{1} hrs of {2}:{3} hrs time remaining<br/>", (bu.RemainingUpgradeTime / 60), (bu.RemainingUpgradeTime % 60), (bu.TotalUpgradeTime / 60), (bu.TotalUpgradeTime % 60));
                
                foreach (var requiredProduct in bu.Products)
                {
                    var prodType = prodTypes.First(x => x.Id == requiredProduct.ProductTypeId);
                    var strgProd = city.CurrentCityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == requiredProduct.ProductTypeId);
                    
                    var reequiredProductTootip = new StringBuilder();
                    if (strgProd != null)
                    {
                        requiredProduct.StorageQuantity = strgProd.Quantity ?? 0;
                        requiredProduct.RequiredProducts = Enumerable.ToArray(prodType.RequiredProducts.ToArray().Select(Mapper.Map<Product>)); 
                        var notInStorage = strgProd.Quantity < requiredProduct.Quantity;
                        var style = notInStorage
                            ? "display: block; color: red"
                            : "display: block";
                        buildingUpgradeTooltip.AppendFormat("<span style='{0}'>{1} {2} required of {3} in storage</span>", style, requiredProduct.Quantity, requiredProduct.Name, strgProd.Quantity);
                        reequiredProductTootip.AppendFormat("<span style='{0}'>{1} {2} required of {3} in storage</span>", style, requiredProduct.Quantity, requiredProduct.Name, strgProd.Quantity);
                        if (notInStorage)
                        {

                            foreach (var requiredSubProduct in requiredProduct.RequiredProducts)
                            {
                                var subStrgProd = city.CurrentCityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == requiredSubProduct.ProductTypeId);
                                if (subStrgProd != null)
                                {
                                    requiredSubProduct.StorageQuantity = subStrgProd.Quantity ?? 0;
                                    var totalQuantityRequired = requiredSubProduct.Quantity * requiredProduct.Quantity;
                                    var subStyle = subStrgProd.Quantity < totalQuantityRequired
                                        ? "display: block; color: red"
                                        : "display: block";
                                    reequiredProductTootip.AppendFormat("<span style='{0}'>{1} {2} required of {3} in storage</span>", subStyle, totalQuantityRequired, subStrgProd.Name, subStrgProd.Quantity);
                                }
                                else
                                {
                                    buildingUpgradeTooltip.AppendFormat("<span style='display: block'>{0} {1} required", requiredSubProduct.Quantity,
                                        prodTypes.First(x => x.Id == requiredSubProduct.ProductTypeId).Name);
                                }
                            }
                        }
                    }
                    else
                        buildingUpgradeTooltip.AppendFormat("<span style='display: block'>{0} {1} required", requiredProduct.Quantity, requiredProduct.Name);
                    
                    requiredProduct.RequiredProductsToolTip = reequiredProductTootip.ToString();
                }
                buildingUpgradeTooltip.Append("</div>");
                bu.Tooltip = buildingUpgradeTooltip.ToString();
            }
        }

        private void UpdateKeywords(Product[] products, ProductType[] prodTypes, BuildingUpgrade[] buildingUpgrades, BuildingUpgrade bu)
        {
            foreach (var prod in products)
            {
                var pt = prodTypes.FirstOrDefault(x => x.Id == prod.ProductTypeId);
                if (pt != null)
                {
                    
                    prod.Keywords.Add(pt.ManufacturerType.Name);
                }
                if (buildingUpgrades != null)
                foreach (var ug in buildingUpgrades.Where(x => x.Products.Any(y => y.ProductTypeId == prod.ProductTypeId)))
                {
                    prod.Keywords.Add(ug.Name);
                }
                if (bu != null)
                    prod.Keywords.Add(bu.Name);
            }
        }

        private static void UpdateProductStorageQuantity(IEnumerable<Product> collection, Product storageProduct)
        {
            if (collection == null)
                return;
            var updProd = collection.FirstOrDefault(x => x.ProductTypeId == storageProduct.ProductTypeId);
            if (updProd != null)
                updProd.StorageQuantity = storageProduct.Quantity;
        }
    }
}