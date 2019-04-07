using Recipes.Bo;
using Recipes.Dto;

namespace Recipes.Common
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            CreateMap<RecipeIngredient, RecipeIngredientDto>().ReverseMap();

            CreateMap<Recipe, RecipeDto>().ReverseMap();
        }
    }
}
