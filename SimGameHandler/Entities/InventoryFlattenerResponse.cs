using System;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class InventoryFlattenerResponse
    {
        public Product[] Products { get; set; }
    }
}