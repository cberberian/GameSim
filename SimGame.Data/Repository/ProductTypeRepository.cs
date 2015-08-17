using System.Data.Entity;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Repository
{
    public class ProductTypeRepository : AbstractRepository<ProductType>, IProductTypeRepository
    {
      
        protected override IDbSet<ProductType> RepositoryDbSet
        {
            get { return Context.ProductTypes; }
        }
    }
}