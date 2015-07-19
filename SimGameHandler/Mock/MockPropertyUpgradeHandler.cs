using System;
using SimGame.Handler.Entities.Legacy;
using SimGame.Handler.Interfaces;

namespace SimGame.Handler.Mock
{
    public class MockPropertyUpgradeHandler : IPropertyUpgradeHandler
    {
        public PropertyUpgradeHandlerResponse CalculateBuildQueue(PropertyUpgradeHandlerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}