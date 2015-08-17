using System.Data.Entity;
using System.Linq;
using SimGame.Data.Entity;
using SimGame.Data.Interface;

namespace SimGame.Data.Repository
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected abstract IDbSet<T> RepositoryDbSet { get; }
        public IGameSimContext Context { get; set; }
        public IQueryable<T> Get()
        {
            return RepositoryDbSet;
        }

        public IQueryable<T> Get(RepositoryRequest<T> request)
        {
            return RepositoryDbSet.Where(request.Expression);
        }

        public void Add(T entity)
        {
            RepositoryDbSet.Add(entity);
        }

        public void SetValues(T dest, T chng)
        {
            Context.SetValues(dest, chng);
        }
    }
}