using System.Configuration;
using SimGame.Data.Interface;

namespace SimGame.Data.UnitOfWork
{
    public class PropertyUpgradeUoW : IPropertyUpgradeUoW
    {
        public PropertyUpgradeUoW(IProductTypeRepository productTypeRepository, IGameSimContext context)
        {
            productTypeRepository.Context = context;
            ProductTypeRepository = productTypeRepository;
        }

        public IProductTypeRepository ProductTypeRepository { get; set; }
    }
}