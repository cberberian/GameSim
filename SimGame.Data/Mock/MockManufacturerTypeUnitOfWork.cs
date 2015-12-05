using SimGame.Data.Interface;

namespace SimGame.Data.Mock
{
    public class MockManufacturerTypeUnitOfWork : IManufacturerTypeUnitOfWork
    {
        public IManufacturerTypeRepository ManufacturerTypeRepository
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}