using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Calculators
{
    public class PropertyUpgradeDurationCalculator : IPropertyUpgradeDurationCalculator
    {
        private readonly IPropertyUpgradeUoW _propertyUpgradeUoW;
        private ProductType[] _productTypes;

        public PropertyUpgradeDurationCalculator(IPropertyUpgradeUoW propertyUpgradeUoW)
        {
            _propertyUpgradeUoW = propertyUpgradeUoW;
        }


        public PropertyUpgradeDurationCalculatorResponse CalculateUpgradeTime(PropertyUpgradeDurationCalculatorRequest upgradeDurationCalculatorRequest)
        {
            if (upgradeDurationCalculatorRequest == null || 
                upgradeDurationCalculatorRequest.BuildingUpgrade == null ||
                upgradeDurationCalculatorRequest.BuildingUpgrade.Products == null)
                return new PropertyUpgradeDurationCalculatorResponse();
            var typesAlreadyAdded = new List<int>();

            if (upgradeDurationCalculatorRequest.ProductTypes == null)
                upgradeDurationCalculatorRequest.ProductTypes = _propertyUpgradeUoW.ProductTypeRepository.Get().Select(Mapper.Map<ProductType>).ToArray();

            _productTypes = upgradeDurationCalculatorRequest.ProductTypes;
            var duration = upgradeDurationCalculatorRequest.BuildingUpgrade.Products.Sum(item => CalculateInventoryItemDuration(item, typesAlreadyAdded));

            return new PropertyUpgradeDurationCalculatorResponse
            {
                TotalUpgradeTime = duration
            };
        }

        private int CalculateInventoryItemDuration(Product item, ICollection<int> typesAlreadyAdded)
        {
            if (item.Quantity < 1)
                return 0;
            var productType = _productTypes.FirstOrDefault(x => x.Id.Equals(item.ProductTypeId));
            if (productType == null)
                return 0;

            var duration = 0;
            var supportsParallelManufacturing = productType.ManufacturerType.SupportsParallelManufacturing;

            //if supports parrallel then we want the duration to only be the duration of one item because
            //  they'll all be processed at the same time. 
            if (supportsParallelManufacturing)
            {
                //Don't add the type if we've alreay added it. 
                if (typesAlreadyAdded.All(x => item.ProductTypeId != x))
                    // ReSharper disable once PossibleInvalidOperationException
                    duration += productType.TimeToManufacture ?? 0;
            }
            else  //otherwise we add each one * its quantity
            {
                duration += ((productType.TimeToManufacture ?? 0) * (item.Quantity ?? 0));
            }

            //now recurse to add prerequisites
            duration += productType.RequiredProducts.Sum(preReqItem => CalculateInventoryItemDuration(preReqItem, typesAlreadyAdded));
            return duration;
        }
    }
}