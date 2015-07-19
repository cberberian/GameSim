using System.Collections.Generic;

namespace SimGame.Domain
{
    public class BuildingUpgrade
    {
        public int Id { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public void AddProduct(ICollection<Product> products)
        {
            foreach(var prod in products)
                AddProduct(prod);
        }

        private void AddProduct(Product product)
        {
            if (Products == null)
                Products = new List<Product>();
            Products.Add(product);
        }
    }
}