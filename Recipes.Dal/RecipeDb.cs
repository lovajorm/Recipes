using Microsoft.EntityFrameworkCore;
using Recipes.Bo;
using Recipes.Dal.Api;

namespace Recipes.Dal
{
    public class RecipeDb : DbContext
    {
        public RecipeDb(DbContextOptions<RecipeDb> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
