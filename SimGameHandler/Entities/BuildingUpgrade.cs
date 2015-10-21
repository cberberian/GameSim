using System;
using System.Collections.Generic;
using cb.core.domain;

namespace SimGame.Handler.Entities
{
    [Serializable
    ]
    public class BuildingUpgrade : DomainObject
    {
        public BuildingUpgrade()
        {
            Products = new List<Product>();
        }

        public ICollection<Product> Products { get; set; }
        public int TotalUpgradeTime { get; set; }
        public int RemainingUpgradeTime { get; set; }
        public int BuildingUpgradeId { get; set; }
        public bool Completed { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public Product[] ProductsInStorage { get; set; }
        public override int Id { get; set; }
        public bool CalculateInBuildingUpgrades { get; set; }

        public void AddRequiredUpgradeItem(Product product)
        {
            Products.Add(product);
        }
    }
}