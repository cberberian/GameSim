using System.Linq;
using AutoMapper;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Handlers
{
    public class PropertyUpgradeHandler : IPropertyUpgradeHandler
    {
        private readonly IPropertyUpgradeDurationCalculator _propertyUpgradeDurationCalculator;
        private readonly IPropertyUpgradeUoW _propertyUpgradeUoW;
        private ProductType[] _productTypes;
        private readonly IBuildingUpgradProductConsolidator _buildingUpgradeProductConsolidator;


        public PropertyUpgradeHandler(IPropertyUpgradeDurationCalculator propertyUpgradeDurationCalculator, IPropertyUpgradeUoW propertyUpgradeUoW, IBuildingUpgradProductConsolidator buildingUpgradeProductConsolidator)
        {
            _propertyUpgradeDurationCalculator = propertyUpgradeDurationCalculator;
            _propertyUpgradeUoW = propertyUpgradeUoW;
            _buildingUpgradeProductConsolidator = buildingUpgradeProductConsolidator;
        }

        public PropertyUpgradeHandlerResponse CalculateBuildQueue(PropertyUpgradeHandlerRequest request)
        {
            //Early out if we don't have required data. 
            if (request == null || request.City == null || request.City.Manufacturers == null)
                return new PropertyUpgradeHandlerResponse
                {
                    RequiredProductQueue = new Product[]{}
                };
            var handlerResponse = new PropertyUpgradeHandlerResponse
            {
                CityStorage = request.City.CurrentCityStorage
            };

            var productTypes = _propertyUpgradeUoW.ProductTypeRepository.Get().AsEnumerable().ToArray();
            _productTypes = productTypes.Select(Mapper.Map<ProductType>).ToArray();

            //Get property upgrade orders in the order requested (currently only sorted by shortest to longest duration)
            //  should be able to sort by priority etc... 
            handlerResponse.OrderedUpgrades = ReorderUpgradesByDuration(request);

            if (request.ReturnInventory)
            {
                //Get the list of inventory items from the orders including prerequisites to be queued.
                var buildingUpgradeProductConsoldatorRequest = new BuildingUpgradeProductConsoldatorRequest()
                {
                    BuildingUpgrades = handlerResponse.OrderedUpgrades,
                    CityStorage = handlerResponse.CityStorage,
                    ProductTypes = _productTypes
                };
                var buildingUpgradeProductConsolidatorResponse = _buildingUpgradeProductConsolidator.GetConsolidatedProductQueue(buildingUpgradeProductConsoldatorRequest);
                handlerResponse.RequiredProductQueue =  buildingUpgradeProductConsolidatorResponse.ConsolidatedRequiredProductQueue.ToArray();
                handlerResponse.TotalProductQueue =  buildingUpgradeProductConsolidatorResponse.ConsolidatedTotalProductQueue.ToArray();
                handlerResponse.AvailableStorage =  buildingUpgradeProductConsolidatorResponse.AvailableStorage.ToArray();
            }

            //now create manufacturing order to be placed.
            //  NOTE only creates one order where it maxes out supply chain limits. 
//            List<LegacyProduct> pendingInventory;
//            var manufactingOrder = CreateManufacturingOrder(request, orderedInventoryQueue, out pendingInventory);

            return handlerResponse;
        }

/*
        /// <summary>
        /// This returns a product queue that contains a combined/aggregated product list to show citywide what products 
        ///     need to be queued
        /// </summary>
        /// <param name="orderedPropUpgrades"></param>
        /// <param name="cityStorage"></param>
        /// <returns></returns>
        private Queue<Product> GetConsolidatedCityWideProductQueue(IEnumerable<BuildingUpgrade> orderedPropUpgrades, CityStorage cityStorage)
        {
            var combinedList = new List<Product>();
            //first flatten the list including the dependencies.
            var list = orderedPropUpgrades.SelectMany(upgrade => GetFlattenedInventory(upgrade.Products)).ToList();

            //now combine the redundant queued products into one product per product type
            foreach(var productType in _productTypes)
            {
                var productTypeId = productType.Id;
                var totalInStorage = GetTotalInStorage(productType, cityStorage);
                Product combinedItem = null;
                var itemAdded = false;
                var queuedProducts = list.Where(x => x.ProductTypeId.HasValue && x.ProductTypeId.Value == productTypeId).ToArray();
                var count = queuedProducts.Count();
                if (count > 0)
                foreach (var queuedProduct in queuedProducts)
                {
                    if (combinedItem == null)
                    {
                        combinedItem = new Product(productType);
                    }
                    //totalNeeded = totalWanted - whatsInStorage
                    var quantityToAdd = combinedItem.Quantity + (queuedProduct.Quantity ?? 0) - totalInStorage;
                    if (quantityToAdd <= 0)
                        break; //done with product type because we have the rest in storage.
                    combinedItem.Quantity += quantityToAdd;

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
                if (combinedItem != null)
                    combinedList.Add(combinedItem);
                
            }
            //return the list in order by manufacturer type id then by how long to fulfill the item. 
            var ret = new Queue<Product>(combinedList.OrderBy(x=>x.ManufacturerTypeId).ThenBy(y=>y.TimeToFulfill));
            return ret; 
        }
*/

        /// <summary>
        /// Get Upgrade Orders ordered by the order that takes the least time to process.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private BuildingUpgrade[] ReorderUpgradesByDuration(PropertyUpgradeHandlerRequest request)
        {
            var requestPropUpgrades = request.City.BuildingUpgrades;
            foreach (var propUpgrade in requestPropUpgrades)
            {
                var upgradeDurationCalculatorRequest = new PropertyUpgradeDurationCalculatorRequest
                {
                    BuildingUpgrade = propUpgrade,
                    ProductTypes = _productTypes
                };
                var propUpgradeDurationCalcResponse = _propertyUpgradeDurationCalculator.CalculateUpgradeTime(upgradeDurationCalculatorRequest);
                propUpgrade.TotalUpgradeTime = propUpgradeDurationCalcResponse.TotalUpgradeTime;
            }

            var orderedPropUpgrades = requestPropUpgrades.OrderBy(x => x.TotalUpgradeTime).ToArray();
            return orderedPropUpgrades;
        }


        //        private static ManufacturingOrder CreateManufacturingOrder(PropertyUpgradeHandlerRequest request,
        //            Queue<LegacyProduct> orderedInventoryQueue, out List<LegacyProduct> pendingInventory)
        //        {
        //            var manufactingOrder = new ManufacturingOrder
        //            {
        //                BuildingFacilities = request.City.CurrentBuildingFacilities.OrderBy(x => x.FacilityType).ThenBy(y => y.QueueSize).ToArray()
        //            };
        //
        //            pendingInventory = new List<LegacyProduct>();
        //            while (orderedInventoryQueue.Count > 0)
        //            {
        //                var inventoryItem = orderedInventoryQueue.Dequeue();
        //                //no facility for that type. 
        //                if (manufactingOrder.BuildingFacilities.Any(x =>
        //                    x.FacilityType == inventoryItem.FacilityType &&
        //                    x.QueueSize > x.ManufacturingQueue.Count))
        //                {
        //                    foreach (var facilityToAssignTo in manufactingOrder.BuildingFacilities.Where(x =>
        //                        x.FacilityType == inventoryItem.FacilityType &&
        //                        x.QueueSize > x.ManufacturingQueue.Count))
        //                    {
        //                        var totalThatCanBeAdded = facilityToAssignTo.QueueSize - facilityToAssignTo.ManufacturingQueue.Count;
        //                        //set to either the max that can be added or the total count if all can be manufactured. 
        //                        totalThatCanBeAdded = totalThatCanBeAdded > inventoryItem.Quantity.Value
        //                            ? inventoryItem.Quantity.Value
        //                            : totalThatCanBeAdded;
        //                        for (var a = 0; a < totalThatCanBeAdded; a++)
        //                            facilityToAssignTo.ManufacturingQueue.Enqueue(inventoryItem);
        //
        //                        inventoryItem.Quantity -= totalThatCanBeAdded;
        //                    }
        //                }
        //                if (inventoryItem.Quantity > 0)
        //                    pendingInventory.Add(inventoryItem);
        //            }
        //            return manufactingOrder;
        //        }
        //

    }
}