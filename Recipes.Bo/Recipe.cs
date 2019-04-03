using System.Collections.Generic;

namespace Recipes.Bo
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}