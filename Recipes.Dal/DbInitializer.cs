using Microsoft.EntityFrameworkCore.Internal;
using Recipes.Bo;

namespace Recipes.Dal
{
    public static class DbInitializer
    {
        //This class seeds the database with some testdata
        public static void Initialize(RecipeDb recipeDb)
        {
            recipeDb.Database.EnsureCreated();

            //Look for any recipes
            if (recipeDb.Recipes.Any())
            {
                return; //DB has been seeded
            }

            var category = new Category[]
            {
                new Category{CategoryName = "Desserts"}, 
                new Category{CategoryName = "Soups"}, 
                new Category{CategoryName = "Vegetarian"},
            };
            foreach (Category c in category)
            {
                recipeDb.Categories.Add(c);
            }
            recipeDb.SaveChanges();


            var ingredient = new Ingredient[]
            {
                new Ingredient{Name = "Olive oil"},
                new Ingredient{Name = "Onion"},
                new Ingredient{Name = "Tomato"},
                new Ingredient{Name = "Garlic"},
                new Ingredient{Name = "Basil"},
                new Ingredient{Name = "Lentil"},
                new Ingredient{Name = "Butter"},
                new Ingredient{Name = "Egg"},
                new Ingredient{Name = "Cocoa powder"},
                new Ingredient{Name = "Sugar"},
                new Ingredient{Name = "Flour"},
                new Ingredient{Name = "Salt"},
            };
            foreach (Ingredient i in ingredient)
            {
                recipeDb.Ingredients.Add(i);
            }
            recipeDb.SaveChanges();

            var recipe = new Recipe[]
            {
                new Recipe{Name = "Tomato Soup", Description = "Add the onion and carrots and saute for 8-10 minutes, until tender. Add the garlic and cook for 1 minute. Add the tomatoes, tomato paste, basil, chicken stock, salt, and pepper and stir well. Bring the soup to a boil, lower the heat, and simmer, uncovered, for 30 minutes, until the tomatoes are very tender.",
                    CategoryId = 2, Difficulty = Difficulty.Easy},
                new Recipe{Name = "Vegetable Lasagna", Description = "Stir the ricotta cheese, 1/2 cup Parmesan cheese and eggs in a medium bowl and set it aside.  Season the beef as desired.  In a 3-quart saucepan over medium-high heat, cook the beef until it's well browned, stirring often to break up the meat. Pour off any fat. Stir the sauce in the saucepan.",
                    CategoryId = 3, Difficulty = Difficulty.Hard},
                new Recipe{Name = "Brownies", Description = "Preheat oven to 350 degrees F. Line a metal 9x9 pan with parchment paper. Pour melted butter into a large mixing bowl. Whisk in sugar by hand until smooth, 30 seconds. Add in eggs and vanilla extract. Whisk 1 minute. Whisk in melted chocolate until combined and smooth. Use a rubber spatula to stir in flour, cocoa powder, and salt until just combined. Stir in whole chocolate chips. Pour into prepared pan and smooth out. Bake in the preheated oven for 30 minutes.Let cool in pan 30 minutes before slicing.",
                    CategoryId = 1, Difficulty = Difficulty.Intermediate},
                new Recipe{Name = "Lentil soup", Description = "Preheat oven to 350 degrees F. Line a metal 9x9 pan with parchment paper. Pour melted butter into a large mixing bowl. Whisk in sugar by hand until smooth, 30 seconds. Add in eggs and vanilla extract. Whisk 1 minute. Whisk in melted chocolate until combined and smooth. Use a rubber spatula to stir in flour, cocoa powder, and salt until just combined. Stir in whole chocolate chips. Pour into prepared pan and smooth out. Bake in the preheated oven for 30 minutes.Let cool in pan 30 minutes before slicing.",
                    CategoryId = 2, Difficulty = Difficulty.Easy},
            };
            foreach (Recipe r in recipe)
            {
                recipeDb.Recipes.Add(r);
            }
            recipeDb.SaveChanges();
        }
    }
}
