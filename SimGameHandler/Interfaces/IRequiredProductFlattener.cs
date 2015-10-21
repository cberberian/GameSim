using SimGame.Handler.Entities;

namespace SimGame.Handler.Interfaces
{
    public interface IRequiredProductFlattener
    {
        RequiredProductFlattenerResponse GetFlattenedInventory(RequiredProductFlattenerRequest requiredProductFlattenerRequest);
    }
}