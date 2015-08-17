using System.Collections.Generic;

namespace SimGame.Domain
{
    public class Order : DomainObject
    {
        public override int Id { get; set; }
        public string OrderDescription { get; set; }
        public ICollection<Product> Products { get; set; }
        
    }
}