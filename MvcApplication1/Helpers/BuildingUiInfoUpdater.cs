using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MvcApplication1.Interfaces;
using MvcApplication1.Models;
using Product = SimGame.Domain.Product;
using ProductType = SimGame.Domain.ProductType;

namespace MvcApplication1.Helpers
{
    public class BuildingUiInfoUpdater : IBuildingUiInfoUpdater
    {
        public void Update(BuildingUpgrade[] buildingUpgrades, CityStorage currentCityStorage, ProductType[] prodTypes)
        {
            foreach (var bu in buildingUpgrades)
            {
                var buildingUpgradeTooltip = new StringBuilder();
                buildingUpgradeTooltip.Append("<div>");
                //add total hours remaining
                buildingUpgradeTooltip.AppendFormat("{0}:{1} hrs of {2}:{3} hrs time remaining<br/>", (bu.RemainingUpgradeTime / 60), (bu.RemainingUpgradeTime % 60), (bu.TotalUpgradeTime / 60), (bu.TotalUpgradeTime % 60));
                
                foreach (var requiredProduct in bu.Products)
                {
                    var prodType = prodTypes.First(x => x.Id == requiredProduct.ProductTypeId);
                    var strgProd = currentCityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == requiredProduct.ProductTypeId);
                    
                    var reequiredProductTootip = new StringBuilder();
                    if (strgProd != null)
                    {
                        requiredProduct.StorageQuantity = strgProd.Quantity ?? 0;
                        requiredProduct.RequiredProducts = prodType.RequiredProducts.ToArray().Select(Mapper.Map<Models.Product>).ToArray(); 
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
                                var subStrgProd = currentCityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == requiredSubProduct.ProductTypeId);
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
    }
}