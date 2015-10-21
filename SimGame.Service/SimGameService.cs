using System.ServiceModel;
using SimGame.Contract;

namespace SimGame.Service
{
    [ServiceBehavior]
    public class SimGameService : ISimGameService
    {
        public QueueProductForProductionResponse QueueProductForProduction(QueueProductForProductionRequest request)
        {
            return new QueueProductForProductionResponse();
        }
    }
}
