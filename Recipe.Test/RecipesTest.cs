using Xunit;
using Moq;
using Recipes.Dal.Api;
using Recipes.Dto;
using Recipes.Web.Controllers;
using System.Collections.Generic;
using Recipes.Bo;
using Recipes.Bo.Enum;

namespace Recipe.Test
{
    public class RecipeTest
    {
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


        public RecipeDto GetRecipe()
        {
            var recipeIngredients = new List<RecipeIngredientDto>
            {
                new RecipeIngredientDto
                {
                    RecipeId = 5,
                    IngredientId = 1,
                    Value = 2,
                    Measure = Measure.cup
                },
                new RecipeIngredientDto
                {
                    RecipeId = 5,
                    IngredientId = 2,
                    Value = 2,
                    Measure = Measure.cup
                },
                new RecipeIngredientDto
                {
                    RecipeId = 5,
                    IngredientId = 3,
                    Value = 2,
                    Measure = Measure.cup
                }
            };

            var recipeDto = new RecipeDto()
            {
                Id = 5,
                Name = "TestRecept",
                Description = "TestTest",
                CategoryId = 1,
                Difficulty = Difficulty.Easy,
                RecipeIngredients = recipeIngredients
            };
            return recipeDto;
        }


        [Fact]
        public void Test()
        {
            var mock = new Mock<IRecipeRepository>();

            mock.Setup(x => x.SumRecipePrice(5)).Returns(125);

            mock.Object.SumRecipePrice(5);

            mock.Verify(x => x.SumRecipePrice(5));
        }
    }
}
