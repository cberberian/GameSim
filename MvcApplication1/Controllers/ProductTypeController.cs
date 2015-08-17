using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AutoMapper;
using MvcApplication1.Models;
using SimGame.Data;

namespace MvcApplication1.Controllers
{
    public class ProductTypeController : ApiController
    {
        private readonly GameSimContext _db = new GameSimContext();
        // GET api/producttype
        [Queryable()]
        public IQueryable<ProductType> Get(ODataQueryOptions<SimGame.Domain.ProductType> paramters)
        {
            var resultset = paramters.ApplyTo(_db.ProductTypes).AsQueryable() as IQueryable<SimGame.Domain.ProductType>;
            // ReSharper disable once AssignNullToNotNullAttribute
            return resultset.ToArray().Select(Mapper.Map<ProductType>).AsQueryable();
        }

        // POST api/producttype
        public void Post([FromBody]string value)
        {
        }

        // PUT api/producttype/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/producttype/5
        public void Delete(int id)
        {
        }
    }
}
