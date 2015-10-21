using System.ServiceModel;

namespace SimGame.Contract
{
    [ServiceContract]
    public interface ISimGameService
    {
        [OperationContract]
        QueueProductForProductionResponse QueueProductForProduction(QueueProductForProductionRequest request);
    }
}