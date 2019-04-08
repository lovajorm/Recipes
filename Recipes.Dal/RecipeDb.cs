using Microsoft.EntityFrameworkCore;
using Recipes.Bo;

namespace Recipes.Dal
{
    public class RecipeDb : DbContext
    {
        public RecipeDb(DbContextOptions<RecipeDb> options) : base(options) { }

        //Setting a combined primary key in RecipeIngredient
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RecipeIngredient>().HasKey(t => new { t.RecipeId, t.IngredientId });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
