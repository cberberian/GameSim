using System;

namespace SimGame.Handler.Entities
{
    [Serializable]
    public class ManufacturerTypeSearchHandlerResponse
    {
        public ManufacturerType[] Results { get; set; }
    }
}