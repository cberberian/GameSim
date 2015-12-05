using System.Collections.Generic;
using System.Linq;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;
using ProductType = SimGame.Domain.ProductType;

namespace SimGame.Handler.Calculators
{
    public class CityStorageCalculator : ICityStorageCalculator
    {
        private readonly IPropertyUpgradeUoW _propertyUpgradeUoW;
        private ProductType[] _productTypes;

        public CityStorageCalculator(IPropertyUpgradeUoW propertyUpgradeUoW)
        {
            _propertyUpgradeUoW = propertyUpgradeUoW;
        }


        public CalculateStorageResponse CalculateNewStorageAmounts(CalculateStorageRequest calculateStorageRequest)
        {
            if (ValidateRequest(calculateStorageRequest))
                return new CalculateStorageResponse
                {
                    CityStorage = calculateStorageRequest.CityStorage
                };
            _productTypes = GetProductTypes(calculateStorageRequest);
            foreach (var updateProduct in calculateStorageRequest.NewProductQuantities)
            {
                AddTopLevelProductToCityStorage(updateProduct, calculateStorageRequest.CityStorage);
            }
            return new CalculateStorageResponse
            {
                CityStorage = calculateStorageRequest.CityStorage
            };
        }

        private ProductType[] GetProductTypes(CalculateStorageRequest calculateStorageRequest)
        {
            if (calculateStorageRequest.ProductTypes != null)
                return calculateStorageRequest.ProductTypes;
            return _propertyUpgradeUoW.ProductTypeRepository.Get().ToArray();
        }

        private static bool ValidateRequest(CalculateStorageRequest calculateStorageRequest)
        {
            return calculateStorageRequest == null || calculateStorageRequest.CityStorage == null || 
                    calculateStorageRequest.CityStorage.CurrentInventory == null ||
                    calculateStorageRequest.NewProductQuantities == null;
        }

        private void AddTopLevelProductToCityStorage(Product updateProduct, CityStorage cityStorage)
        {
            if (updateProduct.Quantity == 0)
                return;
            var prodType = _productTypes.FirstOrDefault(x => x.Id == updateProduct.ProductTypeId);
            var currentCityStorageProduct =
                cityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == updateProduct.ProductTypeId);
            if (currentCityStorageProduct == null)
                return;
            //Add top level amount
            currentCityStorageProduct.Quantity += updateProduct.Quantity;
            if (prodType == null)
                return; 
            //now subtract the required products it took to create the top level product. 
            for (int i = 0; i < updateProduct.Quantity; i++)
            {
                SubtractChildProductsFromCityStorage(prodType.RequiredProducts, cityStorage);
            }
            

        }

        private void SubtractChildProductsFromCityStorage(ICollection<Domain.Product> requiredProducts, CityStorage cityStorage)
        {
            foreach (var prd in requiredProducts)
            {
                var cityStorageProduct =
                    cityStorage.CurrentInventory.FirstOrDefault(x => x.ProductTypeId == prd.ProductTypeId);
                if (cityStorageProduct == null)
                    continue;
                cityStorageProduct.Quantity -= prd.Quantity;
                if (cityStorageProduct.Quantity < 0)
                    cityStorageProduct.Quantity = 0;
                var requiredProductType = _productTypes.FirstOrDefault(x => x.Id == prd.ProductTypeId);
                if (requiredProductType == null ||  requiredProductType.RequiredProducts == null)
                    continue;
//                SubtractChildProductsFromCityStorage(requiredProductType.RequiredProducts, cityStorage);
            }
        }
    }
}