using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Recipes.Bo;
using Recipes.Dal;
using Recipes.Dal.Api;

namespace Recipes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private IRecipeRepository _recipeRepository;
        private readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public RecipesController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET api/recipes
        [HttpGet]
        public ActionResult<List<Recipe>> Get()
        {
            var recipes = _recipeRepository.GetAllRecipes();
            _log.Info("Listing all recipes.");
            return recipes;
        }

        // GET api/recipes/5
        [HttpGet("{id}")]
        public ActionResult<Recipe> Get(int id)
        {
            var recipe = _recipeRepository.GetRecipe(id);
            _log.Info("Getting a recipe by id.");
            return recipe;
        }

        // GET api/recipes/category/5
        [HttpGet("category/{id}")]
        public ActionResult<List<Recipe>> GetByCategory(int id)
        {
            var recipes = _recipeRepository.GetByCategory(id);
            _log.Info("Listing all recipes by given categoryid.");
            return recipes;
        }

        // GET api/recipes/name/5
        [HttpGet("name/{name}")]
        public ActionResult<Recipe> GetByName(string name)
        {
            var recipe = _recipeRepository.GetRecipeByName(name);
            _log.Info("Listing all recipes by given recipename.");
            return recipe;
        }

        // POST api/recipes
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
