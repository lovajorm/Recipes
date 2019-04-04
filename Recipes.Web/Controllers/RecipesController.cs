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
        public ActionResult<string> Get()
        {
            var recipes = _recipeRepository.GetAllRecipes();

            string jsonData = JsonConvert.SerializeObject(recipes);

            _log.Info("Listing all recipes.");
            return jsonData;
        }

        // GET api/recipes/5
        //Get a recipe by id
        [HttpGet("{id:int}")]
        public ActionResult<Recipe> Get(int id)
        {
            var recipe = _recipeRepository.GetRecipe(id);

            JsonConvert.SerializeObject(recipe, Formatting.Indented);

            _log.Info("Getting a recipe by id.");
            return recipe;
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


        // POST api/recipes
        [HttpPost("create")]
        public HttpResponseMessage CreateRecipe([FromBody]Recipe recipe)
        {
            try
            {
                //var recipe = _mapper.Map<Recipe>(recipeDto);
                _recipeRepository.CreateRecipe(recipe);
                _log.Info("Creating a new recipe.");
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                _log.Error($"Failed to create recipe. {e}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }



        // PUT api/recipes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/recipes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
