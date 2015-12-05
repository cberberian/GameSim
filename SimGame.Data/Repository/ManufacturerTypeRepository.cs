using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using cb.core.data;
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
            return _context.ManufacturerTypes
                            .Include(x=>x.ProductTypes); 
        }

        public IQueryable<ManufacturerType> Get(RepositoryRequest<ManufacturerType> request)
        {
            return _context.ManufacturerTypes.Where(request.Expression);
        }

        public void Add(ManufacturerType entity)
        {
            throw new NotImplementedException();
        }

        public void SetValues(ManufacturerType dest, ManufacturerType chng)
        {
            throw new NotImplementedException();
        }
    }
}