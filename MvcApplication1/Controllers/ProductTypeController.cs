using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AutoMapper;
using cb.core.data;
using SimGame.Data.Interface;
using SimGame.WebApi.Models;

namespace SimGame.WebApi.Controllers
{
    public class ProductTypeController : ApiController
    {
        private readonly IGameSimContext _db;
        public ProductTypeController(IGameSimContext db)
        {
            _db = db;
        }


        // GET api/producttype
        [Queryable()]
        public IQueryable<ProductType> Get(ODataQueryOptions<Domain.ProductType> paramters)
        {
            var resultset = paramters.ApplyTo(_db.ProductTypes).AsQueryable() as IQueryable<Domain.ProductType>;
            // ReSharper disable once AssignNullToNotNullAttribute
            var productTypes = resultset.ToArray().Select(Mapper.Map<ProductType>).AsQueryable();
            return productTypes;
        }

        // POST api/producttype
        public ProductType Post([FromBody]ProductType value)
        {
            if (value == null)
                return null;
            var mappedProductType = Mapper.Map<Domain.ProductType>(value);
            var domainObject = _db.ProductTypes.FirstOrDefault(x => x.Id == mappedProductType.Id);
            if (domainObject == null)
            {
                var newId = _db.ProductTypes.Max(x => x.Id) + 1;
                domainObject = Mapper.Map<Domain.ProductType>(mappedProductType);
                domainObject.Id = newId;
                _db.ProductTypes.Add(domainObject);
                _db.Products.Add(new Domain.Product
                {
                    Quantity = 0,
                    ProductTypeId = newId,
                    IsCityStorage = true
                });
                foreach (var prod in domainObject.RequiredProducts)
                    prod.RequiredByTypeId = newId;
            }
            else
            {
                _db.SetValues(domainObject, mappedProductType);
            }
            DataCollectionMapper.MapCollection(mappedProductType.RequiredProducts, domainObject.RequiredProducts, new CollectionMapperOptions
            {
                Context = _db
            });
            _db.Commit();

            return Mapper.Map<ProductType>(domainObject);

        }

        // PUT api/producttype/5
        public void Put(int id, [FromBody]ProductType value)
        {
        }

        // DELETE api/producttype/5
        public void Delete(int id)
        {
        }
    }
}
