﻿using System.Collections.Generic;
using Recipes.Bo;
using Recipes.Dto;

namespace Recipes.Dal.Api
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAllRecipes();

        Recipe GetRecipe(int id);

        List<Recipe> GetByCategory(string name);

        Recipe GetRecipeByName(string name);

        List<Recipe> GetRecipeByIngredient(string name);

        void CreateRecipe(RecipeDto recipeDto);

        float SumRecipePrice(int id);

        Recipe DoesRecipeExist(int id);

        void DeleteRecipe(int id);
    }
}
