using SimGame.Data.Interface;

namespace SimGame.Data.UnitOfWork
{
    public class CityUpdateUnitOfWork : AbstractUnitOfWork, ICityUpdateUnitOfWork
    {
        public CityUpdateUnitOfWork(IProductRepository productRepository, IBuildingUpgradeRepository buildingUpgradeRepositoryRepository, IGameSimContext context) : base(context)
        {
            buildingUpgradeRepositoryRepository.Context = context;
            BuildingUpgradeRepository = buildingUpgradeRepositoryRepository;
            productRepository.Context = context;
            ProductRepository = productRepository;
        }
        public IProductRepository ProductRepository { get; set; }
        public IBuildingUpgradeRepository BuildingUpgradeRepository { get; set; }
    }
}