using System.Linq;
using SimGame.Data.Entity;

namespace SimGame.Data.Interface
{
    public interface IRepository<T>
    {
        IGameSimContext Context { get; set; }
        IQueryable<T> Get();
        IQueryable<T> Get(RepositoryRequest<T> request);
    }
}