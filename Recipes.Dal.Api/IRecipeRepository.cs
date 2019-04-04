using System.Collections.Generic;
using Recipes.Bo;

namespace Recipes.Dal.Api
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAllRecipes();

        Recipe GetRecipe(int id);

        List<Recipe> GetByCategory(int id);

        Recipe GetRecipeByName(string name);

        Recipe CreateRecipe(Recipe recipe);
    }
}
