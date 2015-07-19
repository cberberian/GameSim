using SimGame.Handler.Entities;
using SimGame.Handler.Entities.Legacy;

namespace SimGame.Handler.Interfaces
{
    public interface IManufacturerTypeSearchHandler
    {
        ManufacturerTypeSearchHandlerResponse Get(ManufacturerTypeSearchHandlerRequest request);
    }
}