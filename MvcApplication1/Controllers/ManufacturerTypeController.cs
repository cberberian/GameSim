using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using MvcApplication1.Models;
using SimGame.Handler.Interfaces;
using HandlerEntities = SimGame.Handler.Entities;

namespace MvcApplication1.Controllers
{
    public class ManufacturerTypeController : ApiController
    {
        private readonly IManufacturerTypeSearchHandler _manufacturerTypeHandler;

        public ManufacturerTypeController(IManufacturerTypeSearchHandler manufacturerTypeHandler)
        {
            _manufacturerTypeHandler = manufacturerTypeHandler;
        }

        // GET api/InventoryItems
        public IEnumerable<ManufacturerType> Get()
        {
            try
            {
                var request = new HandlerEntities.ManufacturerTypeSearchHandlerRequest();
                var manufacturerTypeSearchHandlerResponse = _manufacturerTypeHandler.Get(request);
                return manufacturerTypeSearchHandlerResponse.Results.Select(Mapper.Map<ManufacturerType>);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        // GET api/InventoryItems/5
        public ManufacturerType Get(int id)
        {
            var request = new HandlerEntities.ManufacturerTypeSearchHandlerRequest();
            var manufacturerTypeSearchHandlerResponse = _manufacturerTypeHandler.Get(request);
            return Mapper.Map<ManufacturerType>(manufacturerTypeSearchHandlerResponse.Results.FirstOrDefault());
        }
    }
}
