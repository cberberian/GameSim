using cb.core.interfaces;
using SimGame.Data.Repository;
using SimGame.Domain;

namespace SimGame.Data.Interface
{
    public interface IProductRepository : IRepository<IGameSimContext, Product>
    {
    }
}