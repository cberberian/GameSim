using System;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class RequiredProductFlattenerResponse
    {
        public Product[] Products { get; set; }
    }
}