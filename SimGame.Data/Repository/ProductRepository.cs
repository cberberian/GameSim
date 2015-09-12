using System.Data.Entity;
using cb.core.data;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Repository
{
    public class ProductRepository : AbstractRepository<IGameSimContext, Product>, IProductRepository
    {

        protected override IDbSet<Product> RepositoryDbSet
        {
            get { return Context.Products; }
        }
    }
}