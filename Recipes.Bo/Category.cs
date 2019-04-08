using System.ComponentModel.DataAnnotations;

namespace Recipes.Bo
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category needs a name...")]
        [MaxLength(50)]
        public string CategoryName { get; set; }
    }
}