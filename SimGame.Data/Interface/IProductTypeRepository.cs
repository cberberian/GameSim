using cb.core.interfaces;
using SimGame.Domain;

namespace SimGame.Data.Interface
{
    public interface IProductTypeRepository : IRepository<IGameSimContext, ProductType>
    {
    }
}