using System.ComponentModel.DataAnnotations;

namespace MvcApplication2.Models
{
    public class ProductType
    {
        public int? Id { get; set; }
        
        [Required(ErrorMessage = "The name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The time to manufacture is required.")]
        [Display(Name = "Time to Manufacture")]
        public int? TimeToManufacture { get; set; }
        [Required(ErrorMessage = "The manufacturer type is required.")]
        [Display(Name = "Manufacturer Type")]
        public int? ManufacturerTypeId { get; set; }
        [Display(Name = "Required Products")]
        public Product[] RequiredProducts { get; set; }
        public string RequiredProductsToolTip { get; set; }
         
    }

}