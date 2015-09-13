using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AutoMapper;
using SimGame.Data;
using SimGame.WebApi.Models;

namespace SimGame.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly GameSimContext _db = new GameSimContext();
        // GET api/default1
        [Queryable()]
        public IQueryable<Product> Get(ODataQueryOptions<SimGame.Domain.Product> paramters)
        {
            var resultset = paramters.ApplyTo(_db.Products).AsQueryable() as IQueryable<SimGame.Domain.Product>;
            // ReSharper disable once AssignNullToNotNullAttribute
            return resultset.ToArray().Select(Mapper.Map<Product>).AsQueryable();
        }

        // POST api/default1
        public void Post([FromBody]string value)
        {
        }

        // PUT api/default1/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/default1/5
        public void Delete(int id)
        {
        }
    }
}
