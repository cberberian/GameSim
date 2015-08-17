using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Calculators
{
    public class BuildingUpgradeDurationCalculator : IBuildingUpgradeDurationCalculator
    {
        private readonly IPropertyUpgradeUoW _propertyUpgradeUoW;
        private ProductType[] _productTypes;

        public BuildingUpgradeDurationCalculator(IPropertyUpgradeUoW propertyUpgradeUoW)
        {
            _propertyUpgradeUoW = propertyUpgradeUoW;
        }


        public BuildingUpgradeDurationCalculatorResponse CalculateUpgradeTime(BuildingUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest)
        {
            if (InValidBuildingUpgrades(upgradeDurationCalculatorRequest))
                return new BuildingUpgradeDurationCalculatorResponse();

            InitializeProductTypes(upgradeDurationCalculatorRequest);

            //first calculate the total times. This also adds child required products 
            var totalUpgradeTime = GetTotalUpgradeTime(upgradeDurationCalculatorRequest);
            //now calculate the remaining time based on city storage. This will update the already added child required products 
            var remainingUpgradeTime = GetRemainingUpgradeTime(upgradeDurationCalculatorRequest);
            
            //update the building upgrade totals
            upgradeDurationCalculatorRequest.BuildingUpgrade.TotalUpgradeTime = totalUpgradeTime;
            upgradeDurationCalculatorRequest.BuildingUpgrade.RemainingUpgradeTime = remainingUpgradeTime;

            return new BuildingUpgradeDurationCalculatorResponse
            {
                TotalUpgradeTime = totalUpgradeTime,
                RemainingUpgradeTime = remainingUpgradeTime
            };
        }

        /// <summary>
        /// - Calculates the total upgrade time for the building upgrade and 
        /// - Adds the required sub products to the required products and updates their product total times 
        /// </summary>
        /// <param name="upgradeDurationCalculatorRequest"></param>
        /// <returns></returns>
        private int GetTotalUpgradeTime(BuildingUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest)
        {
            var retTime = 0;
            var addedTypes = new List<int>();
            foreach (var item in upgradeDurationCalculatorRequest.BuildingUpgrade.Products)
            {
                var productTotalDuration = CalculateInventoryItemDuration(item, addedTypes, true, null);
                item.TotalDuration = productTotalDuration;
                retTime += productTotalDuration;
            }
            return retTime;
        }

        private void InitializeProductTypes(BuildingUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest)
        {
            if (upgradeDurationCalculatorRequest.ProductTypes == null)
                upgradeDurationCalculatorRequest.ProductTypes =
                    _propertyUpgradeUoW.ProductTypeRepository.Get().Select(Mapper.Map<ProductType>).ToArray();
            _productTypes = upgradeDurationCalculatorRequest.ProductTypes;
        }

        private static bool InValidBuildingUpgrades(BuildingUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest)
        {
            return upgradeDurationCalculatorRequest == null || 
                   upgradeDurationCalculatorRequest.BuildingUpgrade == null ||
                   upgradeDurationCalculatorRequest.BuildingUpgrade.Products == null;
        }

        /// <summary>
        /// Calculates the specified products duration based on the product types duration and quantity. 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parallelTypesAlreadyAdded"></param>
        /// <param name="includeRequiredProducts"></param>
        /// <param name="cityStorage"></param>
        /// <returns></returns>
        private int CalculateInventoryItemDuration(Product item, ICollection<int> parallelTypesAlreadyAdded, bool includeRequiredProducts, CityStorage cityStorage)
        {
            if (item.Quantity < 1)
                return 0;
            var productType = GetProductType(item);
            if (productType == null)
                return 0;

            var duration = 0;
            var quantity = item.Quantity.HasValue ? item.Quantity.Value < 0 ? 0 : item.Quantity.Value : 0;
            if (item.RequiredProducts == null)
                item.RequiredProducts = productType.RequiredProducts.Select(x => x.Clone()).ToArray();
            for (var a = 0; a < quantity; a++)
            {
                duration += CalculateInventoryItemDurationByType(productType, parallelTypesAlreadyAdded, includeRequiredProducts, cityStorage, item.RequiredProducts);
            }
            
            if (cityStorage == null)
            {
                item.TotalDuration += duration;
            }
            else
            {
                item.RemainingDuration += duration;
            }
            return duration;
        }

        private ProductType GetProductType(Product item)
        {
            return _productTypes.FirstOrDefault(x => x.Id.Equals(item.ProductTypeId));
        }

        public BuildingUpgradeDurationCalculatorResponse CalculateRemainingTime(
            BuildingUpgradeDurationCalculatorRequest durationRequest)
        {
            if (InValidBuildingUpgrades(durationRequest))
                return new BuildingUpgradeDurationCalculatorResponse();
            InitializeProductTypes(durationRequest);

            CalculateUpgradeTime(durationRequest);
            var remainingUpgradeTime = GetRemainingUpgradeTime(durationRequest);

            return new BuildingUpgradeDurationCalculatorResponse
            {
                RemainingUpgradeTime = remainingUpgradeTime
            };
        }

        private int GetRemainingUpgradeTime(BuildingUpgradeDurationCalculatorRequest durationRequest)
        {
            ICollection<int> typesAlreadyAdded = new List<int>();
            var sum = 0;
            foreach (var item in durationRequest.BuildingUpgrade.Products)
            {
                var productRemainingDuration = CalculateInventoryItemDuration(item, typesAlreadyAdded, true, durationRequest.CityStorage.Clone());
                item.RemainingDuration = productRemainingDuration;
                sum += productRemainingDuration;
            }
            return sum;
        }


        /// <summary>
        /// Recurses through required products and calculates the total time required to produce the product 
        ///     based on product type duration and quanity of child products required. If city storage  
        ///     is specified then the amount in storage is subrtacted from the total durations and the
        ///     quantity of items in storage is decremented to reflect the use of the required products. 
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="typesAlreadyAdded"></param>
        /// <param name="includeRequiredTypes"></param>
        /// <param name="cityStorage">If included then duration is decreased by what's in storage.</param>
        /// <param name="productsToUpdate"></param>
        /// <returns></returns>
        private int CalculateInventoryItemDurationByType(ProductType productType, ICollection<int> typesAlreadyAdded, bool includeRequiredTypes, CityStorage cityStorage, Product[] productsToUpdate)
        {
            
            // if we have one in storage then we consider the duration already having been elapsed.
            var cityStorageSpecified = cityStorage != null;
            var cityStorageDecremented = DecrementCityStorageByProductType(productType, cityStorage);
            if (cityStorageDecremented)
                return 0;

            var productTypeTotalDuration = CalculateTimeToManufacture(productType, typesAlreadyAdded);

            if (!includeRequiredTypes) 
                return productTypeTotalDuration;
            
            foreach (var prod in productsToUpdate)
            {
                var productTotalDuration = 0;

                //product types should always have any product type.
                var requiredProductType = _productTypes.First(x => x.Id == prod.ProductTypeId);
                if (prod.RequiredProducts == null)
                    prod.RequiredProducts = requiredProductType.RequiredProducts.Select(x => x.Clone()).ToArray();
                for (var a = 0; a < prod.Quantity; a++)
                {
                    productTotalDuration += CalculateInventoryItemDurationByType(requiredProductType, typesAlreadyAdded, true, cityStorage, prod.RequiredProducts);
                }

                //if city storage is not null then set remaining time 
                if (cityStorageSpecified)
                {
                    if (!prod.RemainingDuration.HasValue)
                        prod.RemainingDuration = 0;
                    prod.RemainingDuration += productTotalDuration;
                }
                else
                {
                    if (!prod.TotalDuration.HasValue)
                        prod.TotalDuration = 0;
                    prod.TotalDuration += productTotalDuration;
                }
                productTypeTotalDuration += productTotalDuration;

            }
            return productTypeTotalDuration; 
        }

        private static int CalculateTimeToManufacture(ProductType productType, ICollection<int> typesAlreadyAdded)
        {
            //if supports parallel manufacturing then we only need to include the duration once. 
            var supportsParallelManufacturing = productType.ManufacturerType.SupportsParallelManufacturing;
            var parallelDuration = 0;
            if (supportsParallelManufacturing)
            {
                if (typesAlreadyAdded.All(x => x != productType.Id))
                {
                    //if its not in city storage then add it to the total duration
                    parallelDuration = productType.TimeToManufacture ?? 0;
                    typesAlreadyAdded.Add(productType.Id);
                }
            }
            else
                parallelDuration = productType.TimeToManufacture ?? 0;
            return parallelDuration;
        }

        private static bool DecrementCityStorageByProductType(ProductType productType, CityStorage cityStorage)
        {
            if (cityStorage==null) return false;
            var storageProduct = cityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == productType.Id);
            if (storageProduct == null || storageProduct.Quantity <= 0) return false;
            
            storageProduct.Quantity -= 1;
            return true;
        }

    }
}