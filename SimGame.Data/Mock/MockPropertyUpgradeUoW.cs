using SimGame.Data.Interface;

namespace SimGame.Data.Mock
{
    public class MockPropertyUpgradeUoW : IPropertyUpgradeUoW
    {
        public IProductTypeRepository ProductTypeRepository { get; set; }
    }
}