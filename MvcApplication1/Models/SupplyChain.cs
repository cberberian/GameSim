namespace MvcApplication1.Models
{
    public class SupplyChain
    {
        public Product[] RequiredProducts { get; set; }
        public CityStorage CurrentCityStorage { get; set; }
        public Product[] TotalProducts { get; set; }
        public Product[] AvailableStorage { get; set; }
    }
}