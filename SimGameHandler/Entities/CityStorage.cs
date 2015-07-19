using System;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class CityStorage
    {
        public int Capacity { get; set; }
        public Product[] CurrentInventory { get; set; }
    }
}