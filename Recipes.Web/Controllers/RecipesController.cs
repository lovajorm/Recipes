using System;
using System.Collections.Generic;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Recipes.Bo;
using Recipes.Dal.Api;
using AutoMapper;
using Recipes.Dto;

namespace Recipes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private IRecipeRepository _recipeRepository;
        private readonly ILog _log = LogManager.GetLogger(typeof(Program));
        private readonly IMapper _mapper;

        public RecipesController(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        // GET api/recipes
        //List all recipes
        [HttpGet]
        public ActionResult<List<Recipe>> Get()
        {
            try
            {
                var recipes = _recipeRepository.GetAllRecipes();
                _log.Info("Listing all recipes.");
                return recipes;
            }
            catch (Exception e)
            {
                _log.Error($"Failed to list recipes. {e}");
                return BadRequest("Failed to list recipes.");
            }
        }

        // GET api/recipes/5
        //Get a recipe by id
        [HttpGet("{id:int}")]
        public ActionResult<RecipeDto> GetById(int id)
        {
            try
            {
                //Check to see if recipe exist and if not tell user it doesn't 
                var recipeExist = _recipeRepository.DoesRecipeExist(id);
                if (!recipeExist)
                    return NotFound("Recipe does not Exist");

                var recipe = _recipeRepository.GetRecipe(id);

                //Mapping Recipe to RecipeDto so we can show recipePrice
                var dest = _mapper.Map<Recipe, RecipeDto>(recipe);

                var finaldest = _recipeRepository.GetUnitPriceToRecipeIngredient(dest);

                //Calling method to sum ingredients and get total recipe price
                var price = _recipeRepository.SumRecipePrice(id);
                dest.RecipePrice = price;

                _log.Info("Getting a recipe by id.");
                return finaldest;
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get recipe. {e}");
                return BadRequest("Failed to get recipe.");
            }
        }

        // GET api/recipes/category/5
        //List all recipes by category
        [HttpGet("category/{name}")]
        public ActionResult<List<Recipe>> GetByCategory(string name)
        {
            try
            {
                var recipes = _recipeRepository.GetByCategory(name);
                _log.Info("Listing all recipes by given category name.");
                return recipes;
            }
            catch (Exception e)
            {
                _log.Error($"Failed to list recipes by category. {e}");
                return BadRequest("Failed to list recipes by category.");
            }
        }

        // GET api/recipes/name/tomato soup
        //Get a recipe by recipe name
        [HttpGet("recipe/{name}")]
        public ActionResult<Recipe> GetByName(string name)
        {
            try
            {
                var recipe = _recipeRepository.GetRecipeByName(name);
                _log.Info("Listing all recipes by given recipename.");
                return recipe;
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get recipe by name. {e}");
                return BadRequest("Failed to get recipe by name.");
            }
        }

        // GET api/recipes/name/tomato soup
        //Get a recipe by recipe name
        [HttpGet("ingredient/{name}")]
        public ActionResult<List<Recipe>> GetByIngredient(string name)
        {
            try
            {
                var recipes = _recipeRepository.GetRecipeByIngredient(name);
                _log.Info("Listing all recipes by ingredient.");
                return recipes;
            }
            catch (Exception e)
            {
                _log.Error($"Failed to list recipes by ingredient. {e}");
                return BadRequest("Failed to list recipes by ingredient.");
            }
        }


        // POST api/recipes/create
        [HttpPost("create")]
        public IActionResult CreateRecipe([FromBody]RecipeDto recipeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _recipeRepository.CreateRecipe(recipeDto);
                _log.Info("Creating a new recipe.");
                return Ok($"Recipe was created successfully :)");
            }
            catch (Exception e)
            {
                _log.Error($"Failed to create recipe. {e}");
                return BadRequest($"Failed to create recipe. {e}");
            }
        }

        // POST api/recipes/price/5
        [HttpGet("price/{id}")]
        public ActionResult<float> GetRecipePrice(int id)
        {
            try
            {
                //Check to see if recipe exist and if not tell user it doesn't 
                var recipeExist = _recipeRepository.DoesRecipeExist(id);
                if (!recipeExist)
                    return BadRequest("Recipe does not Exist");

                var price = _recipeRepository.SumRecipePrice(id);
                _log.Info("Sums up recipe price.");
                return price;
            }
            catch (Exception e)
            {
                _log.Error($"Failed to get recipe price. {e}");
                return BadRequest("Failed to get recipe price.");
            }
        }


        // DELETE api/recipes/5
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                //Check to see if recipe exist and if not tell user it doesn't 
                var recipeExist = _recipeRepository.DoesRecipeExist(id);
                if (!recipeExist)
                    return BadRequest("Recipe does not Exist");

                _recipeRepository.DeleteRecipe(id);
                _log.Info($"Recipe with {id} was deleted successfully :)");

                return Ok("Recipe is deleted :)");
            }
            catch (Exception e)
            {
                _log.Error($"Failed to delete recipe. {e}");
                return BadRequest($"Failed to delete recipe. {e}");
            }
        }
    }
}


//[Fact]
//public void RecipeSumTest()
//{
//var mock = new Mock<IRecipeRepository>();
//var mock2 = new Mock<IIngredientRepository>();

//var recipeIngredients = new List<RecipeIngredient>();
//recipeIngredients.Add(new RecipeIngredient { IngredientId = 1, Measure = Measure.tbs, Value = 15 });
//recipeIngredients.Add(new RecipeIngredient { IngredientId = 2, Measure = Measure.tbs, Value = 30 });
//recipeIngredients.Add(new RecipeIngredient { IngredientId = 3, Measure = Measure.tbs, Value = 45 });

//var recipe = new Recipes.Bo.Recipe
//{
//    RecipeIngredients = recipeIngredients
//};

//mock.Setup(x => x.GetRecipe(5)).Returns(recipe);

//mock2.Setup(x => x.GetIngredient(It.IsAny<int>())).Returns(new Ingredient { UnitPrice = 15 });

//var contr = new RecipesController(mock.Object, null, mock2.Object);

//var result = contr.SumRecipePrice(5);

//Assert.Equal(45, result);
//}

//public float SumRecipePrice(int id)
//{
////list all recipeingredients with same recipeId
//var ingredientList = _recipeRepository.GetRecipe(id).RecipeIngredients;
//var ingredients = new List<Ingredient>();

//    foreach (var ingredient in ingredientList)
//{
//    ingredients.Add(_ingredientRepository.GetIngredient(ingredient.IngredientId));
//}

//var price = ingredients.Sum(x => x.UnitPrice);

//    return price;
//}