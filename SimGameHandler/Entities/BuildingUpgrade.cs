using System;
using System.Collections.Generic;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class BuildingUpgrade 
    {
        public BuildingUpgrade()
        {
            Products = new List<Product>();
        }

        public int TotalUpgradeTime { get; set; }

        public ICollection<Product> Products { get; set; }

        public void AddRequiredUpgradeItem(Product product)
        {
            Products.Add(product);
        }
    }
}