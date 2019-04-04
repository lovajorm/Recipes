using System.Collections.Generic;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Recipes.Bo;
using Recipes.Dal.Api;

namespace Recipes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private IIngredientRepository _ingredientRepository;
        private readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public IngredientsController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        // GET api/ingredients
        //List all ingredients
        [HttpGet]
        public ActionResult<List<Ingredient>> Get()
        {
            var ingredient = _ingredientRepository.GetAllIngredients();
            _log.Info("Listing all ingredients.");
            return ingredient;
        }

        // GET api/ingredients/5
        //Get a ingredient by id
        [HttpGet("{id}")]
        public ActionResult<Ingredient> Get(int id)
        {
            var ingredient = _ingredientRepository.GetIngredient(id);
            _log.Info("Getting a ingredient by id.");
            return ingredient;
        }
    }
}