using System.Runtime.Serialization;

namespace SimGame.Contract
{
    [DataContract]
    public class QueueProductForProductionRequest
    {
        public int ProductTypeId { get; set; }
        public int Quanity { get; set; }
    }
}
