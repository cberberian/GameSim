using System.Linq;
using AutoMapper;
using SimGame.Data.Entity;
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
                return new ManufacturerTypeSearchHandlerResponse
                {
                    Results = Enumerable.ToArray(_manufacturerTypeUnitOfWork.ManufacturerTypeRepository.Get(CreateRepositoryRequest(request)).Select(Mapper.Map<ManufacturerType>))
                };
            return new ManufacturerTypeSearchHandlerResponse
            {
                Results = Enumerable.ToArray(_manufacturerTypeUnitOfWork.ManufacturerTypeRepository.Get().Select(Mapper.Map<ManufacturerType>))
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