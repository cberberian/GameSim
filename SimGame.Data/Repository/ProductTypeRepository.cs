using System.Data.Entity;
using System.Linq;
using cb.core.data;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Repository
{
    public class ProductTypeRepository : AbstractRepository<IGameSimContext, ProductType>, IProductTypeRepository
    {
        public override IQueryable<ProductType> Get()
        {
            return base.Get()
                .Include(x=>x.ManufacturerType)
                .Include(y=>y.RequiredProducts);
        }

        protected override IDbSet<ProductType> RepositoryDbSet
        {
            get { return Context.ProductTypes; }
        }
    }
}