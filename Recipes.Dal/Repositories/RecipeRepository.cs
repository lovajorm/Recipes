using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using log4net;
using Recipes.Bo;
using Recipes.Dal.Api;

namespace Recipes.Dal.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private RecipeDb _db;
        private readonly IMapper _mapper;
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RecipeRepository(RecipeDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //List all recipes
        public List<Recipe> GetAllRecipes()
        {
            var recipes = _db.Recipes.ToList();
            return recipes;
        }

        //Get a recipe by Id
        public Recipe GetRecipe(int id)
        {
            var recipe = _db.Recipes.FirstOrDefault(r => r.Id == id);
            return recipe;
        }

        //List all recipes by category
        public List<Recipe> GetByCategory(int id)
        {
            var recipes = _db.Recipes.Where(r => r.CategoryId == id).ToList();
            return recipes;
        }

        //Get a recipe by name
        public Recipe GetRecipeByName(string name)
        {
            var recipe = _db.Recipes.Where(r => r.Name == name).FirstOrDefault();
            return recipe;
        }

        //List all recipes by ingredient
        public List<Recipe> GetRecipeByIngredient(string ingredient)
        {
            //var recipe = _db.Recipes.Where(r => r.RecipeIngredients.Contains(recipeIngredient)).ToList();

            //var recipes = _db.Recipes.Where(r => r.RecipeIngredients)

            //var recipes = _db.Recipes.Where(r => r.RecipeIngredients = ingredient).ToList();
            return new List<Recipe>();
        }

        //Create a new recipe
        public void CreateRecipe(Recipe recipe)
        {
            var recipes = _db.Recipes.FirstOrDefault(r => r.Id == recipe.Id);
            if (recipes == null)
            {
                var recipeIngredients = new List<RecipeIngredient>();

                foreach (var ingredient in recipe.RecipeIngredients)
                {
                    var recipeIngredient2 = new RecipeIngredient()
                    {
                        RecipeId = ingredient.RecipeId,
                        IngredientId = ingredient.IngredientId,
                        Value = ingredient.Value,
                        Measure = ingredient.Measure
                    };
                    recipeIngredients.Add(recipeIngredient2);
                }

                var newRecipe = new Recipe()
                {
                    Name = recipe.Name,
                    Description = recipe.Description,
                    CategoryId = recipe.CategoryId,
                    Difficulty = recipe.Difficulty,
                    RecipeIngredients = recipeIngredients
                };

                _db.Recipes.Add(newRecipe);
                _db.SaveChanges();
            }
        }
    }
}
