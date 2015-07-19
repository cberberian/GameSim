using System.Linq;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Repository
{
    public class ProductTypeRepository : AbstractRepository<ProductType>, IProductTypeRepository
    {
        protected override IQueryable<ProductType> RepositoryQueryable
        {
            get { return Context.ProductTypes; }
        }
    }
}