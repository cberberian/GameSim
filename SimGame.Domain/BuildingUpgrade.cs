using System.Collections.Generic;

namespace SimGame.Domain
{
    public class BuildingUpgrade : DomainObject
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public int Priority { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}