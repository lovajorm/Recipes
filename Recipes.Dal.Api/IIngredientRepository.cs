using System.Collections.Generic;
using Recipes.Bo;

namespace Recipes.Dal.Api
{
    public interface IIngredientRepository
    {
        List<Ingredient> GetAllIngredients();

        Ingredient GetIngredient(int id);
    }
}
