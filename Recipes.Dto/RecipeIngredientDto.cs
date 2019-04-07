using Recipes.Bo.Enum;

namespace Recipes.Dto
{
    public class RecipeIngredientDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public Measure Measure { get; set; }
        public float UnitPrice { get; set; }
    }
}
