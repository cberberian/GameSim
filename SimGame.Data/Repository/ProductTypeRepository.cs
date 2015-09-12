using System.Data.Entity;
using cb.core.data;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Repository
{
    public class ProductTypeRepository : AbstractRepository<IGameSimContext, ProductType>, IProductTypeRepository
    {
      
        protected override IDbSet<ProductType> RepositoryDbSet
        {
            get { return Context.ProductTypes; }
        }
    }
}