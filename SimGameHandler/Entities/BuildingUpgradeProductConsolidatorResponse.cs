using System;
using System.Collections.Generic;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class BuildingUpgradeProductConsolidatorResponse
    {
        public Queue<Product> ConsolidatedRequiredProductQueue { get; set; }
        public Queue<Product> ConsolidatedTotalProductQueue { get; set; }
        public List<Product> AvailableStorage { get; set; }
        public List<Product> ConsolidatedRequiredProductsInStorage { get; set; }
    }
}