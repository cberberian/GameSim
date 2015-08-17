using System.Data.Entity;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Repository
{
    public class ProductRepository : AbstractRepository<Product>,  IProductRepository
    {

        protected override IDbSet<Product> RepositoryDbSet
        {
            get { return Context.Products; }
        }
    }
}