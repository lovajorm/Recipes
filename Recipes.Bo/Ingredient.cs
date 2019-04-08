using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Bo
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingredient needs a name...")]
        [MaxLength(50)]
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public IList<RecipeIngredient> Recipes { get; set; }
    }
}