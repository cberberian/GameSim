using System.Collections.Generic;

namespace SimGame.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderDescription { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}