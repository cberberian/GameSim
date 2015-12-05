using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AutoMapper;
using cb.core;
using log4net;
using SimGame.Data;
using SimGame.WebApi.Models;

namespace SimGame.WebApi.Controllers
{
    public static class LoggingTimeStamp
    {
        public static DateTime StartDebugTimeStampedMessage(string msg, ILog logger)
        {
            var theDate = DateTime.Now;
            logger.DebugFormat("{0} {1}: {2}", theDate.ToShortDateString(), theDate.ToLongTimeString(), msg);
            return theDate;
        }
    }

    public class BuildingUpgradeController : ApiController
    {
        private readonly GameSimContext _db = new GameSimContext();
        private ILog _logger;

        private ILog Logger
        {
            get
            {
                if (_logger == null)
                    _logger = LogManager.GetLogger(GetType());
                return _logger;
            }
        }
        // GET api/buildingupgrade
        public IQueryable<BuildingUpgrade> Get(ODataQueryOptions<Domain.BuildingUpgrade> paramters)
        {
            var logStart = LogHelper.StartLog("Started BuildingUpgradeController.Get", Logger);
            var resultset = paramters.ApplyTo(_db.BuildingUpgrades).AsQueryable() as IQueryable<Domain.BuildingUpgrade>;
            // ReSharper disable once AssignNullToNotNullAttribute

            var buildingUpgrades = resultset.ToArray().Where(x=>x.Name.StartsWith("Property") && x.Completed == false).OrderBy(x=>x.Name). Select(Mapper.Map<BuildingUpgrade>).AsQueryable();
            return LogHelper.EndLog(logStart, buildingUpgrades);
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
