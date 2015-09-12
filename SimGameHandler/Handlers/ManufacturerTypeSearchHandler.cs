using System.Linq;
using AutoMapper;
using cb.core.data;
using SimGame.Data.Interface;
using SimGame.Handler.Entities;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Handlers
{
    public class ManufacturerTypeSearchHandler : IManufacturerTypeSearchHandler 
    {
        private readonly IManufacturerTypeUnitOfWork _manufacturerTypeUnitOfWork;

        public ManufacturerTypeSearchHandler(IManufacturerTypeUnitOfWork manufacturerTypeUnitOfWork)
        {
            _manufacturerTypeUnitOfWork = manufacturerTypeUnitOfWork;
        }

        public ManufacturerTypeSearchHandlerResponse Get(ManufacturerTypeSearchHandlerRequest request)
        {
            if (request.Id.HasValue || !string.IsNullOrWhiteSpace(request.Name))
            {
                var queryable = _manufacturerTypeUnitOfWork.ManufacturerTypeRepository.Get(CreateRepositoryRequest(request));
                return new ManufacturerTypeSearchHandlerResponse
                {
                    Results = queryable.Select(Mapper.Map<ManufacturerType>).ToArray()
                };
            }
            var manufacturerTypes = _manufacturerTypeUnitOfWork.ManufacturerTypeRepository.Get().ToArray();
            return new ManufacturerTypeSearchHandlerResponse
            {
                Results = manufacturerTypes.Select(Mapper.Map<ManufacturerType>).ToArray()
            };
        }

        private RepositoryRequest<Domain.ManufacturerType> CreateRepositoryRequest(ManufacturerTypeSearchHandlerRequest request)
        {
            if (request.Id.HasValue)
                return new RepositoryRequest<Domain.ManufacturerType>
                {
                    Expression = x=> x.Id.Equals(request.Id.Value)
                };
            return new RepositoryRequest<Domain.ManufacturerType>
            {
                Expression = x=> x.Name.Equals(request.Name)
            };
        }
    }
}