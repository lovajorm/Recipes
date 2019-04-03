using System.Collections.Generic;
using Recipes.Bo;

namespace Recipes.Dto
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int RecipeId { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}