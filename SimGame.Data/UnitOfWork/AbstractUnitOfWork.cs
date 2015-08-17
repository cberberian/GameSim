using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data.UnitOfWork
{
    public class AbstractUnitOfWork : IUnitOfWork
    {
        protected AbstractUnitOfWork(IGameSimContext context)
        {
            GameSimContext = context;
        }

        public IGameSimContext GameSimContext { get; set; }
        public void Commit()
        {
            GameSimContext.Commit();
        }

    }
}