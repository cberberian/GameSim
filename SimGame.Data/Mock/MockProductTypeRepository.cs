using System;
using System.Linq;
using cb.core.data;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Mock
{
    public class MockProductTypeRepository : IProductTypeRepository
    {
        public IGameSimContext Context { get; set; }
        public IQueryable<ProductType> Get()
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductType> Get(RepositoryRequest<ProductType> request)
        {
            throw new NotImplementedException();
        }

        public void Add(ProductType entity)
        {
            throw new NotImplementedException();
        }

        public void SetValues(ProductType dest, ProductType chng)
        {
            throw new NotImplementedException();
        }
    }
}