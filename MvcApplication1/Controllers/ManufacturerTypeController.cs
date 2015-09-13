using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using SimGame.Handler.Interfaces;
using HandlerEntities = SimGame.Handler.Entities;
using ManufacturerType = SimGame.WebApi.Models.ManufacturerType;
using ProductType = SimGame.WebApi.Models.ProductType;

namespace SimGame.WebApi.Controllers
{
    public class ManufacturerTypeController : ApiController
    {
        private readonly IManufacturerTypeSearchHandler _manufacturerTypeHandler;

        public ManufacturerTypeController(IManufacturerTypeSearchHandler manufacturerTypeHandler)
        {
            _manufacturerTypeHandler = manufacturerTypeHandler;
        }

        // GET api/InventoryItems
        public IQueryable<ManufacturerType> Get()
        {
            try
            {
                var request = new HandlerEntities.ManufacturerTypeSearchHandlerRequest();
                var manufacturerTypeSearchHandlerResponse = _manufacturerTypeHandler.Get(request);
                var manufacturerTypes = manufacturerTypeSearchHandlerResponse.Results.Select(Mapper.Map<ManufacturerType>).ToList();
                var manufacturerType = new ManufacturerType
                {
                    Id = manufacturerTypes.Count,
                    Name = "All",
                    ProductTypes = manufacturerTypeSearchHandlerResponse.Results.SelectMany(x => x.ProductTypes).OrderBy(x => x.Name).Select(Mapper.Map<ProductType>).ToArray()
                };
                manufacturerTypes.Insert(0, manufacturerType);
                
                return manufacturerTypes.AsQueryable();
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
