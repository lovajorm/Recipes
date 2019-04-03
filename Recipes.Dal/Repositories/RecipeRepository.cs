using System.Collections.Generic;
using System.Linq;
using Recipes.Bo;
using Recipes.Dal.Api;

namespace Recipes.Dal.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private RecipeDb _db;

        public RecipeRepository(RecipeDb db)
        {
            _db = db;
        }

        public List<Recipe> GetAllRecipes()
        {
            var recipes = _db.Recipes.ToList();
            return recipes;
        }

        public Recipe GetRecipe(int id)
        {
            var recipe = _db.Recipes.FirstOrDefault(r => r.Id == id);
            return recipe;
        }

        public List<Recipe> GetByCategory(int id)
        {
            var recipes = _db.Recipes.Where(r => r.CategoryId == id).ToList();
            return recipes;
        }

        public Recipe GetRecipeByName(string name)
        {
            var recipe = _db.Recipes.Where(r => r.Name == name).FirstOrDefault();
            return recipe;
        }
    }
}
