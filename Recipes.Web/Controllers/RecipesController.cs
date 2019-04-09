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
            var recipes = _recipeRepository.GetAllRecipes();

            _log.Info("Listing all recipes.");
            return recipes;
        }

        // GET api/recipes/5
        //Get a recipe by id
        [HttpGet("{id:int}")]
        public ActionResult<RecipeDto> GetById(int id)
        {
            //Check to see if recipe exist and if not tell user it doesn't 
            var recipeExist = _recipeRepository.DoesRecipeExist(id);
            if (recipeExist == null)
                return NotFound("Recipe does not Exist");

            var recipe = _recipeRepository.GetRecipe(id);

            //Mapping Recipe to RecipeDto so we can show recipePrice
            var dest = _mapper.Map<Recipe, RecipeDto>(recipe);

            //Calling method to sum ingredients and get total recipe price
            var price = _recipeRepository.SumRecipePrice(id);
            dest.RecipePrice = price;

            _log.Info("Getting a recipe by id.");
            return dest;
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
            var recipe = _recipeRepository.GetRecipeByName(name);
            _log.Info("Listing all recipes by given recipename.");
            return recipe;
        }

        // GET api/recipes/name/tomato soup
        //Get a recipe by recipe name
        [HttpGet("ingredient/{name}")]
        public ActionResult<List<Recipe>> GetByIngredient(string name)
        {
            var recipes = _recipeRepository.GetRecipeByIngredient(name);
            _log.Info("Listing all recipes by ingredient.");
            return recipes;
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
            //Check to see if recipe exist and if not tell user it doesn't 
            var recipeExist = _recipeRepository.DoesRecipeExist(id);
            if (recipeExist == null)
                return BadRequest("Recipe does not Exist");

            var price = _recipeRepository.SumRecipePrice(id);
            _log.Info("Sums up recipe price.");
            return price;
        }


        // DELETE api/recipes/5
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            //Check to see if recipe exist and if not tell user it doesn't 
            var recipeExist = _recipeRepository.DoesRecipeExist(id);
            if (recipeExist == null)
                return BadRequest("Recipe does not Exist");

            _recipeRepository.DeleteRecipe(id);
            _log.Info($"Recipe with {id} was deleted successfully :)");

            return Ok("Recipe is deleted :)");
        }
    }
}
