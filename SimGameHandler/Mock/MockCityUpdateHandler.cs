using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Mock
{
    public class MockCityUpdateHandler : ICityUpdateHandler
    {
        public CityUpdateResponse UpdateCity(CityUpdateRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}