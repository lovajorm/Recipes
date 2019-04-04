using System.Collections.Generic;
using System.Linq;
using Recipes.Bo;
using Recipes.Dal.Api;

namespace Recipes.Dal.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private RecipeDb _db;

        public IngredientRepository(RecipeDb db)
        {
            _db = db;
        }

        public List<Ingredient> GetAllIngredients()
        {
            var ingredient = _db.Ingredients.ToList();
            return ingredient;
        }

        public Ingredient GetIngredient(int id)
        {
            var ingredient = _db.Ingredients.FirstOrDefault(r => r.Id == id);
            return ingredient;
        }
    }
}