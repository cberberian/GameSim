using System.Collections.Generic;

namespace SimGame.Handler.Interfaces
{
    public interface IProduct
    {
        string Name { get; set; }
        int? Quantity { get; set; }
        int? TimeToFulfill { get; set; }
        int? ProductTypeId { get; set; }
        int? TotalDuration { get; set; }
        IEnumerable<IProduct> PreRequisites { get; set; }
        int ManufacturerTypeId { get; set; }
    }
}