using System.Linq;
using AutoMapper;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Handlers
{
    public class BuildingUpgradeHandler : IBuildingUpgradeHandler
    {
        private readonly IBuildingUpgradeDurationCalculator _buildingUpgradeDurationCalculator;
        private readonly IPropertyUpgradeUoW _propertyUpgradeUoW;
        private ProductType[] _productTypes;
        private readonly IBuildingUpgradProductConsolidator _buildingUpgradeProductConsolidator;
        private readonly IBuildingUpgradeStatisticsCalculator _buildingUpgradeStatisticsCalculator;


        public BuildingUpgradeHandler(IBuildingUpgradeDurationCalculator buildingUpgradeDurationCalculator, IPropertyUpgradeUoW propertyUpgradeUoW, IBuildingUpgradProductConsolidator buildingUpgradeProductConsolidator, IBuildingUpgradeStatisticsCalculator buildingUpgradeStatisticsCalculator)
        {
            _buildingUpgradeDurationCalculator = buildingUpgradeDurationCalculator;
            _propertyUpgradeUoW = propertyUpgradeUoW;
            _buildingUpgradeProductConsolidator = buildingUpgradeProductConsolidator;
            _buildingUpgradeStatisticsCalculator = buildingUpgradeStatisticsCalculator;
        }

        public BuildingUpgradeHandlerResponse CalculateBuildQueue(BuildingUpgradeHandlerRequest request)
        {
            //Early out if we don't have required data. 
            if (request == null || request.City == null)
                return new BuildingUpgradeHandlerResponse
                {
                    RequiredProductQueue = new Product[]{},
                    CityStorage = request != null && request.City != null ? request.City.CurrentCityStorage : new CityStorage(),
                    PendingInventory = new Product[0],
                    TotalProductQueue = new Product[0],
                    RequiredProductsInCityStorageQueue = new Product[0],
                    OrderedUpgrades = new BuildingUpgrade[0],
                    AvailableStorage = new Product[0]
                };


            var productTypes = _propertyUpgradeUoW.ProductTypeRepository.Get().AsEnumerable().ToArray();
            
            _productTypes = productTypes.Select(Mapper.Map<ProductType>).ToArray();

            request.City.CurrentCityStorage.CurrentInventory = PopulateRequiredProducts(_productTypes, request.City.CurrentCityStorage.CurrentInventory);
            var handlerResponse = new BuildingUpgradeHandlerResponse
            {
                CityStorage = request.City.CurrentCityStorage,
                //Get property upgrade orders in the order requested (currently only sorted by shortest to longest duration)
                //  should be able to sort by priority etc... 
                OrderedUpgrades = ReorderUpgradesByDuration(request)
            };
            

            //Get the list of inventory items from the orders including prerequisites to be queued.

            if (request.ReturnInventory)
            {
                var buildingUpgradeProductConsolidatorResponse = _buildingUpgradeProductConsolidator.GetConsolidatedProductQueue(new BuildingUpgradeProductConsoldatorRequest
                {
                    BuildingUpgrades = handlerResponse.OrderedUpgrades,
                    CityStorage = handlerResponse.CityStorage,
                    ProductTypes = _productTypes
                });
                handlerResponse.RequiredProductQueue = PopulateRequiredProducts(_productTypes, buildingUpgradeProductConsolidatorResponse.ConsolidatedRequiredProductQueue.ToArray());
                handlerResponse.TotalProductQueue = PopulateRequiredProducts(_productTypes, buildingUpgradeProductConsolidatorResponse.ConsolidatedTotalProductQueue.ToArray());
                handlerResponse.AvailableStorage = PopulateRequiredProducts(_productTypes, buildingUpgradeProductConsolidatorResponse.AvailableStorage.ToArray());
                handlerResponse.RequiredProductsInCityStorageQueue = PopulateRequiredProducts(_productTypes, buildingUpgradeProductConsolidatorResponse.ConsolidatedRequiredProductsInStorage.ToArray());
            }
            else
            {
                handlerResponse.RequiredProductQueue = new Product[0];
                handlerResponse.TotalProductQueue = new Product[0];
                handlerResponse.AvailableStorage = new Product[0];
                handlerResponse.RequiredProductsInCityStorageQueue = new Product[0];
            }

            if (request.CalculateBuidingUpgradeStatistics)
            {
                var statisticsRequest = new BuildingUpgradeStatisticsCalculatorRequest();
                _buildingUpgradeStatisticsCalculator.CalculateStatistics(statisticsRequest);
            }

            //now create manufacturing order to be placed.
            //  NOTE only creates one order where it maxes out supply chain limits. 
//            List<LegacyProduct> pendingInventory;
//            var manufactingOrder = CreateManufacturingOrder(request, orderedInventoryQueue, out pendingInventory);

            return handlerResponse;
        }

        private static Product[] PopulateRequiredProducts(ProductType[] productTypes, Product[] products)
        {
            foreach (var rp in products)
            {
                var pd = productTypes.FirstOrDefault(x => x.Id == rp.ProductTypeId);
                rp.RequiredProducts = pd != null ? pd.RequiredProducts.ToArray() : new Product[0];
            }
            return products;
        }

        /// <summary>
        /// Get Upgrade Orders ordered by the order that takes the least time to process.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private BuildingUpgrade[] ReorderUpgradesByDuration(BuildingUpgradeHandlerRequest request)
        {
            var requestPropUpgrades = request.City.BuildingUpgrades;
//                            var upgradeDurationCalculatorRequest = new BuildingUpgradeDurationCalculatorRequest
//                            {
//                                BuildingUpgrades = requestPropUpgrades,
//                                ProductTypes = _productTypes,
//                                CityStorage = request.City.CurrentCityStorage
//                            };
//            var propUpgradeDurationCalcResponse = _buildingUpgradeDurationCalculator.CalculateUpgradeTimes(upgradeDurationCalculatorRequest);
            foreach (var propUpgrade in requestPropUpgrades)
            {
                var upgradeDurationCalculatorRequest = new BuildingUpgradeDurationCalculatorRequest
                {
                    BuildingUpgrade = propUpgrade,
                    ProductTypes = _productTypes,
                    CityStorage = request.City.CurrentCityStorage
                };
                var propUpgradeDurationCalcResponse = _buildingUpgradeDurationCalculator.CalculateUpgradeTime(upgradeDurationCalculatorRequest);
                propUpgrade.TotalUpgradeTime = propUpgradeDurationCalcResponse.TotalUpgradeTime;
                propUpgrade.RemainingUpgradeTime = propUpgradeDurationCalcResponse.RemainingUpgradeTime;
                propUpgrade.ProductsInStorage = GetProductsAlreadyInStorage(propUpgrade, request.City.CurrentCityStorage.CurrentInventory);
            }

            var orderedPropUpgrades = requestPropUpgrades.OrderBy(x => x.TotalUpgradeTime).ToArray();
            return orderedPropUpgrades;
        }

        private static Product[] GetProductsAlreadyInStorage(BuildingUpgrade propUpgrade, Product[] currentInventory)
        {
            return propUpgrade.Products.Select(prod => currentInventory.FirstOrDefault(x => x.ProductTypeId == prod.ProductTypeId)).Where(strgeProduct => strgeProduct != null).ToArray();
        }
    }
}