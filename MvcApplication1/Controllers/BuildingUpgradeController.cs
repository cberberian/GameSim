﻿using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AutoMapper;
using SimGame.Data;
using SimGame.WebApi.Models;

namespace SimGame.WebApi.Controllers
{
    public class BuildingUpgradeController : ApiController
    {
        private readonly GameSimContext _db = new GameSimContext();
        // GET api/buildingupgrade
        public IQueryable<BuildingUpgrade> Get(ODataQueryOptions<SimGame.Domain.BuildingUpgrade> paramters)
        {
            var resultset = paramters.ApplyTo(_db.BuildingUpgrades).AsQueryable() as IQueryable<SimGame.Domain.BuildingUpgrade>;
            // ReSharper disable once AssignNullToNotNullAttribute
            return resultset.ToArray().Where(x=>x.Name.StartsWith("Property") && x.Completed == false).OrderBy(x=>x.Name). Select(Mapper.Map<BuildingUpgrade>).AsQueryable();
        }

        // POST api/buildingupgrade
        public void Post([FromBody]string value)
        {
        }

        // PUT api/buildingupgrade/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/buildingupgrade/5
        public void Delete(int id)
        {
        }
    }
}
