using SimGame.Data.UnitOfWork;
using SimGame.Domain;

namespace SimGame.Data.Interface
{
    public interface ICityUpdateUnitOfWork : IUnitOfWork
    {
        IProductRepository ProductRepository { get; set; }
        IBuildingUpgradeRepository BuildingUpgradeRepository { get; set; }
    }
}