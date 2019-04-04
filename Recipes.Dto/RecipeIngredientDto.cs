using Recipes.Bo.Enum;

namespace Recipes.Dto
{
    public class RecipeIngredientDto
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public float Value { get; set; }
        public Measure Measue { get; set; }
    }
}
