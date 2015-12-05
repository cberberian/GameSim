using SimGame.Data.Interface;

namespace SimGame.Data.UnitOfWork
{
    public class ManufacturerTypeUnitOfWork : IManufacturerTypeUnitOfWork
    {
        public ManufacturerTypeUnitOfWork(IManufacturerTypeRepository manufacturerTypeRepository)
        {
            ManufacturerTypeRepository = manufacturerTypeRepository;
        }


        public IManufacturerTypeRepository ManufacturerTypeRepository { get; set; }
    }
}