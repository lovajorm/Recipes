using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Recipes.Bo;
using Recipes.Bo.Enum;
using Recipes.Dal.Api;
using Recipes.Dto;

namespace Recipes.Dal.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private RecipeDb _db;
        private readonly IMapper _mapper;

        public RecipeRepository(RecipeDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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

        public Recipe CreateRecipe(Recipe recipe)
        {
            _db.Recipes.Add(new Recipe()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                CategoryId = recipe.CategoryId,
                Difficulty = recipe.Difficulty,
                //RecipeIngredients = new List<RecipeIngredient>()
                //{
                //    new RecipeIngredient{RecipeId = recipe.Id, IngredientId = ingredient.Id, Value = recipeIngredient.Value, Measure = recipeIngredient.Measure},
                //},
            });

            //var ingred = _mapper.Map<List<IngredientDto>>(ingredients);

            AddIngredientsToRecipeIngredients(recipe, recipe.RecipeIngredients);

            _db.SaveChanges();

            //var newRecipe = new Recipe();

            //newRecipe.Name = recipe.Name;
            //newRecipe.Description = recipe.Description;
            //newRecipe.CategoryId = recipe.CategoryId;
            //newRecipe.Difficulty = recipe.Difficulty;

            //_db.Recipes.Add(newRecipe);

            return recipe;
        }

        public void AddIngredientsToRecipeIngredients(Recipe recipe, IList<RecipeIngredient> recipeIngredients)
        {
            var recipeIngredient2 = new RecipeIngredient()
            {
                RecipeId = recipe.Id,
                IngredientId = recipe.RecipeIngredients[0].IngredientId,
                Value = recipe.RecipeIngredients[0].Value,
                Measure = recipe.RecipeIngredients[0].Measure
            };

            //var recipeIng = _mapper.Map<RecipeIngredient>(recipeIngredient2);

            recipeIngredients.Add(recipeIngredient2);
            _db.RecipeIngredients.Add(recipeIngredient2);
            _db.SaveChanges();

            //var recipeIngDto = _mapper.Map<IList<RecipeIngredient>>(recipeIngredients);
            //return recipeIngDto;
        }

        //public List<Recipe> GetRecipeByIngredient(string ingredient)
        //{
        //    var recipes = _db.Recipes.Where(r => r.Ingredients = ingredient).ToList();
        //    return recipes;
        //}
    }
}
