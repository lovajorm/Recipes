using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Recipes.Bo.Enum;

namespace Recipes.Bo
{
    public class RecipeIngredient
    {
        [Key]
        [ForeignKey("Id")]
        public int RecipeId { get; set; }
        [Key]
        [ForeignKey("Id")]
        public int IngredientId { get; set; }
        public float Value { get; set; }
        public Measure Measure { get; set; }
    }
}