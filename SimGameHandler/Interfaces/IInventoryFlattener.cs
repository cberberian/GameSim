using SimGame.Handler.Entities;

namespace SimGame.Handler.Interfaces
{
    public interface IInventoryFlattener
    {
        InventoryFlattenerResponse GetFlattenedInventory(InventoryFlattenerRequest inventoryFlattenerRequest);
    }
}