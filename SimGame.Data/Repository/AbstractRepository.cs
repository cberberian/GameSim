using System.Linq;
using SimGame.Data.Entity;
using SimGame.Data.Interface;

namespace SimGame.Data.Repository
{
    public abstract class AbstractRepository<T> : IRepository<T>
    {
        protected abstract IQueryable<T> RepositoryQueryable { get; }
        public IGameSimContext Context { get; set; }
        public IQueryable<T> Get()
        {
            return RepositoryQueryable;
        }

        public IQueryable<T> Get(RepositoryRequest<T> request)
        {
            return RepositoryQueryable.Where(request.Expression);
        }
    }
}