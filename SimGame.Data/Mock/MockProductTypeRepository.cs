using System.Linq;
using SimGame.Data.Entity;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Mock
{
    public class MockProductTypeRepository : IProductTypeRepository
    {
        public IGameSimContext Context { get; set; }

        public IQueryable<ProductType> Get()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<ProductType> Get(RepositoryRequest<ProductType> request)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ProductType entity)
        {
            throw new System.NotImplementedException();
        }

        public void SetValues(ProductType dest, ProductType chng)
        {
            throw new System.NotImplementedException();
        }
    }
}