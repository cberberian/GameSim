using SimGame.Handler.Entities;

namespace SimGame.Handler.Interfaces
{
    public interface ICityUpdateHandler
    {
        CityUpdateResponse UpdateCity(CityUpdateRequest request);
    }

}