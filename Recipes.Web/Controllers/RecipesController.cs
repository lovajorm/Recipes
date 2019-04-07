using System;
using System.Collections.Generic;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Recipes.Bo;
using Recipes.Dal.Api;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Net;
using AutoMapper;
using Recipes.Dal;
using Recipes.Dto;
using System.Linq;

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
        public ActionResult<List<RecipeDto>> Get()
        {
            var recipes = _recipeRepository.GetAllRecipes();
            //mapping Recipe to RecipeDto
            var listDest = _mapper.Map<List<Recipe>, List<RecipeDto>>(recipes);

            _log.Info("Listing all recipes.");
            return listDest;
        }

        // GET api/recipes/5
        //Get a recipe by id
        [HttpGet("{id:int}")]
        public ActionResult<RecipeDto> Get(int id)
        {
            //Check to see if recipe exist and if not tell user it doesn't 
            var recipeExist = _recipeRepository.DoesRecipeExist(id);
            if (recipeExist == null)
                return BadRequest("Recipe does not Exist");

            var recipe = _recipeRepository.GetRecipe(id);

            //Mapping Recipe to RecipeDto so we can show recipePrice
            var dest = _mapper.Map<Recipe, RecipeDto>(recipe);

            //Calling method to sum ingredients and get total recipe price
            var price = _recipeRepository.CountRecipePrice(id);
            dest.RecipePrice = price;

            _log.Info("Getting a recipe by id.");
            return dest;
        }

        // GET api/recipes/category/5
        //List all recipes by category
        [HttpGet("category/{id:int}")]
        public ActionResult<List<Recipe>> GetByCategory(int id)
        {
            var recipes = _recipeRepository.GetByCategory(id);
            _log.Info("Listing all recipes by given categoryid.");
            return recipes;
        }

        // GET api/recipes/name/tomato soup
        //Get a recipe by recipe name
        [HttpGet("name/{name}")]
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
            //Change to string name of ingred, id for now
            var recipes = _recipeRepository.GetRecipeByIngredient(name);
            _log.Info("Listing all recipes by ingredient.");
            return recipes;
        }


        // POST api/recipes/create
        [HttpPost("create")]
        public HttpResponseMessage CreateRecipe([FromBody]RecipeDto recipeDto)
        {
            try
            {
                _recipeRepository.CreateRecipe(recipeDto);
                _log.Info("Creating a new recipe.");
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to create recipe. {e}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST api/recipes/price/5
        [HttpGet("price/{id}")]
        public ActionResult<float> GetRecipePrice(int id)
        {
            var price = _recipeRepository.CountRecipePrice(id);
            _log.Info("Sums up recipe price.");
            return price;
        }


        // PUT api/recipes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
