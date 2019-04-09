using Xunit;
using Moq;
using Recipes.Dal.Api;
using Recipes.Dto;
using Recipes.Web.Controllers;
using System.Collections.Generic;
using Recipes.Bo;
using Recipes.Bo.Enum;

namespace Recipes.Test
{
    public class RecipeTest
    {
        [Fact]
        public void TestTest()
        {
            
        }

        [Fact]
        public void RecipePriceShouldReturnCorrectValue()
        {
            var mock = new Mock<IRecipeRepository>();

            mock.Setup(x => x.SumRecipePrice(13)).Returns(125);
            
            var contr = new RecipesController(mock.Object, null);
            contr.GetById(5);

            var recipe = GetRecipe().Id;

            var result = mock.Object.SumRecipePrice(recipe);

            Assert.Equal(result, mock.Object.SumRecipePrice(5));
        }
        
        [Fact]
        public void Test()
        {
            var mock = new Mock<IRecipeRepository>();
            var recipe = GetRecipe();

            //var contr = new RecipesController(mock.Object, null);
            //var recipe1 = contr.GetById(recipe.Id);

            var price = mock.Object.SumRecipePrice(recipe.Id);

            //mock.Setup(x => x.SumRecipePrice(recipe.Id)).Returns(55);

            //mock.Object.SumRecipePrice(39);

            //mock.Verify(x => x.SumRecipePrice(39));
        }

        public Recipe GetRecipe()
        {
            var newingredient = new Ingredient()
            {
                Id = 1,
                Name = "TestIngrediens",
                UnitPrice = 20
            };
            var newingredient2 = new Ingredient()
            {
                Id = 2,
                Name = "TestIngrediens2",
                UnitPrice = 10
            };

            var recipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient
                {
                    RecipeId = 5,
                    IngredientId = newingredient.Id,
                    Value = 2,
                    Measure = Measure.cup
                },
                new RecipeIngredient
                {
                    RecipeId = 5,
                    IngredientId = newingredient2.Id,
                    Value = 2,
                    Measure = Measure.cup
                },
            };

            var recipe = new Recipe()
            {
                Id = 5,
                Name = "TestRecept",
                Description = "TestTest",
                CategoryId = 1,
                Difficulty = Difficulty.Easy,
                RecipeIngredients = recipeIngredients
            };
            return recipe;
        }
    }
}
