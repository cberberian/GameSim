using System;
using System.Linq;
using System.Linq.Expressions;
using SimGame.Data.Entity;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.Repository
{
    public class ManufacturerTypeRepository : IManufacturerTypeRepository
    {
        private readonly IGameSimContext _context;

        public ManufacturerTypeRepository(IGameSimContext context)
        {
            _context = context;
        }

        public IGameSimContext Context { get; set; }

        public IQueryable<ManufacturerType> Get()
        {
            return _context.ManufacturerTypes; 
        }

        public IQueryable<ManufacturerType> Get(RepositoryRequest<ManufacturerType> request)
        {
            return _context.ManufacturerTypes.Where(request.Expression);
        }
    }
}