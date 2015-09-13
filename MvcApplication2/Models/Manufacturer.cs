namespace SimGame.Website.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public virtual ManufacturerType ManufacturerType { get; set; }
        public int ManufacturerTypeId { get; set; }
        public int QueueSize { get; set; }
        public string Description { get; set; }
        public bool IsCityStorage { get; set; } 
    }
}