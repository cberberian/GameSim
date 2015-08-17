namespace SimGame.Handler.Entities
{
    public class CalculateStorageRequest
    {
        public Product[] NewProductQuantities { get; set; }
        public CityStorage CityStorage { get; set; }
    }
}