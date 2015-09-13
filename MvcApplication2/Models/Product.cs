using System.ComponentModel.DataAnnotations;

namespace SimGame.Website.Models
{
    public class Product
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "The Quantity for a product is required.")]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "The Product Type for a product is required.")]
        [Display(Name = "Product Type")]
        public int? ProductTypeId { get; set; }
        [Display(Name = "Product Type Name")]
        public string Name { get; set; }
        [Display(Name = "Required By Product type")]
        public int? RequiredByTypeId { get; set; }
        [Display(Name = "Required By Product type")]
        public string RequiredBy { get; set; }

        [Display(Name = "Is City Storage Product")]
        public bool IsCityStorage { get; set; }
        
//        public ProductType[] ProductTypes { get; set; }
    }
}