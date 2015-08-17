using SimGame.Domain;

namespace SimGame.Data.Interface
{
    public interface IUnitOfWork
    {
        IGameSimContext GameSimContext { get; set; }
        void Commit();
    }
}