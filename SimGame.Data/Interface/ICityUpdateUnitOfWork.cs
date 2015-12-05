using cb.core.interfaces;

namespace SimGame.Data.Interface
{
    public interface ICityUpdateUnitOfWork : IUnitOfWork<IGameSimContext>
    {
        IProductRepository ProductRepository { get; set; }
        IBuildingUpgradeRepository BuildingUpgradeRepository { get; set; }
    }
}