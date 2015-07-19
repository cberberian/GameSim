using System.Linq;
using SimGame.Domain;
using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;

namespace MvcApplication1.Models
{
    public class InventoryItem
    {
        public InventoryItem()
        {
            Prerequisites = new InventoryItem[] {};
        }

        public InventoryItem(InventoryItem sourceItem) : this()
        {
            Id = sourceItem.Id;
            Name = sourceItem.Name;
            Quantity = sourceItem.Quantity;
            DurationInMinutes = sourceItem.DurationInMinutes;
            FacilityType = sourceItem.FacilityType;
            Prerequisites = sourceItem.ClonePrerequisites();
        }

        public ProductTypeEnum ItemType { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int DurationInMinutes { get; set; }
        public int FacilityType { get; set; }
        public InventoryItem[] Prerequisites { get; set; }

        public InventoryItem Clone()
        {
            return new InventoryItem
            {
                Id = Id,
                Name = Name,
                Quantity = Quantity,
                DurationInMinutes = DurationInMinutes, 
                FacilityType = FacilityType,
                Prerequisites = ClonePrerequisites()
            };
        }

        internal InventoryItem[] ClonePrerequisites()
        {
            return Prerequisites.Select(x => x.Clone()).ToArray();
        }
    }
}