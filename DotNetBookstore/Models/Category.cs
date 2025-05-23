using System.ComponentModel.DataAnnotations;

namespace DotNetBookstore.Models
{
    public class Category
    {
        //[Range(100, 500, ErrorMessage = "Please enter a valid category ID")]
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Please enter a category name")]
        public string Name { get; set; }
    }
}
