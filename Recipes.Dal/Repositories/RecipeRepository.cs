using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using log4net;
using Recipes.Bo;
using Recipes.Dal.Api;
using Recipes.Dto;

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
        public List<Recipe> GetByCategory(string name)
        {
            var category = _db.Categories.Where(i => i.CategoryName == name).FirstOrDefault();

            var recipes = _db.Recipes.Where(r => r.CategoryId == category.Id).ToList();

            return recipes;
        }

        //Get a recipe by name
        public Recipe GetRecipeByName(string name)
        {
            var recipe = _db.Recipes.Where(r => r.Name == name).FirstOrDefault();
            return recipe;
        }

        //List all recipes by ingredient
        public List<Recipe> GetRecipeByIngredient(string name)
        {
            //Find an ingredient by name
            var ingredient = _db.Ingredients.Where(i => i.Name == name).FirstOrDefault();
            //List recipeingredients with same id as ingredient
            var recipeIngredients = _db.RecipeIngredients.Where(ri => ri.IngredientId == ingredient.Id).ToList();

            //Find every recipe with same Id as the recipeingredients and add to list
            var recipes = new List<Recipe>();
            foreach (var item in recipeIngredients)
            {
                recipes = _db.Recipes.Where(r => r.Id == item.RecipeId).ToList();
            }
            return recipes;
        }


        //Create a new recipe
        public void CreateRecipe(RecipeDto recipeDto)
        {
            var recipeIngredients = new List<RecipeIngredient>();

            //Create ingrenient if it doesnt exist in DB when creating a recipe
            foreach (var ingredient in recipeDto.RecipeIngredients)
            {
                var ingredientExist = _db.Ingredients.Where(i => i.Id == ingredient.IngredientId).FirstOrDefault();
                if (ingredientExist == null)
                {
                    //If ingrediens doesn't exist add it to DB
                    var newIngredient = new Ingredient()
                    {
                        Name = ingredient.Name,
                        UnitPrice = ingredient.UnitPrice
                    };
                    _db.Ingredients.Add(newIngredient);
                    _db.SaveChanges();

                    ingredient.IngredientId = newIngredient.Id;
                }

                //Adding ingredients to RecipeIngredients
                //foreach (var ingredient in recipeDto.RecipeIngredients)
                //{
                var recipeIngredient2 = new RecipeIngredient()
                {
                    RecipeId = ingredient.RecipeId,
                    IngredientId = ingredient.IngredientId,
                    Value = ingredient.Value,
                    Measure = ingredient.Measure
                };
                recipeIngredients.Add(recipeIngredient2);
            }

            //Adding a new recipe
            var newRecipe = new Recipe()
            {
                Name = recipeDto.Name,
                Description = recipeDto.Description,
                CategoryId = recipeDto.CategoryId,
                Difficulty = recipeDto.Difficulty,
                RecipeIngredients = recipeIngredients
            };
            _db.Recipes.Add(newRecipe);
            //Save recipe to DB
            _db.SaveChanges();
        }

        public float SumRecipePrice(int id)
        {
            //list all recipeingredients with same recipeId
            var ingredientList = _db.RecipeIngredients.Where(r => r.RecipeId == id).ToList();

            //Linq join to sum unitPice for every ingredient in a recipe,
            //for every ingredientId with the same recipeId in recipeIngredient
            //sum unitPrice in Ingredient
            var price = ingredientList.Join(_db.Ingredients,
                i => i.IngredientId,
                ig => ig.Id,
                (i, ig) => new { ig.UnitPrice, i.IngredientId }
            ).Sum(i => i.UnitPrice);

            return price;
        }

        //Check if recipe exist in DB
        public Recipe DoesRecipeExist(int id)
        {
            var recipeExist = _db.Recipes.Where(i => i.Id == id).FirstOrDefault();

            return recipeExist;
        }

        //Delete a recipe by id
        public void DeleteRecipe(int id)
        {
            var recipe = _db.Recipes.Where(r => r.Id == id).FirstOrDefault();

            _db.Recipes.Remove(recipe);
            _db.SaveChanges();
        }
    }
}

//sample json to paste into postman to create a recipe http://localhost:56689/api/recipes/create

//{
//"Name": "Chilli tofu stir-fry with soba noodles",
//"Description": "Step 1 Combine tofu, sriracha or chilli sauce, soy sauce and sesame oil in a bowl. Step 2 Cook the soba noodles in a large saucepan of boiling water for 4 mins or until tender. Drain well. Step 3 Heat vegetable oil in a wok or large frying pan over high heat. Drain tofu, reserving the marinade. Cook the tofu, in 2 batches, for 2 mins or until golden. Transfer to a bowl. Add onion to the wok or pan. Stir-fry for 3 mins or until tender. Add baby broccoli and beans. Stir-fry for 2 mins or until tender. Step 4 Return the tofu to the wok or pan with noodles and reserved marinade. Stir-fry until the mixture is heated through. Divide among serving bowls and sprinkle with chilli, if using.",
//"CategoryId": 3,
//"Difficulty": 0,
//"RecipeIngredients": [ { "IngredientId": 4, "Value": 2 },
//{ "IngredientId": 7, "Value": 2, "Measure": 2 },
//{ "Value": 2, "Measure": 2, "Name": "Tofu", "UnitPrice": 55 },
//{ "Value": 2, "Measure": 2, "Name": "Broccoli", "UnitPrice": 15 },
//{ "Value": 2, "Measure": 2, "Name": "Noodles", "UnitPrice": 5 }
//]
//}