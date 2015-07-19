using System;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class ManufacturerTypeSearchHandlerRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}