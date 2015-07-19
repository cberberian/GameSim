using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimGame.Handler.Entities.Legacy;

namespace MvcApplication1.Controllers
{
    public class CityController : ApiController
    {
        // GET api/city
        public IEnumerable<City> Get()
        {
            return new  City[]{};
        }

        // GET api/city/5
        public City Get(int id)
        {
            return new City();
        }

        // POST api/city
        public void Post([FromBody]City value)
        {
        }

        // PUT api/city/5
        public void Put(int id, [FromBody]City value)
        {
        }
          
        // DELETE api/city/5
        public void Delete(int id)
        {
        }
    }
}
