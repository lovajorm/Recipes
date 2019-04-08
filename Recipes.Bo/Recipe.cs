using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Bo
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Recipe needs a name...")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Difficulty Difficulty { get; set; }
        public IList<RecipeIngredient> RecipeIngredients { get; set; }
    }
}