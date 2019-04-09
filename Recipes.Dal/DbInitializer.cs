using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using Recipes.Bo;
using Recipes.Bo.Enum;

namespace Recipes.Dal
{
    public static class DbInitializer
    {
        //This class seeds the database with some testdata
        public static void Initialize(RecipeDb recipeDb)
        {
            recipeDb.Database.EnsureCreated();

            //If there are no recipes seed DB
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
                new Ingredient{Name = "Olive oil", UnitPrice = 40},
                new Ingredient{Name = "Onion", UnitPrice = 5},
                new Ingredient{Name = "Tomato", UnitPrice = 10},
                new Ingredient{Name = "Garlic", UnitPrice = 10},
                new Ingredient{Name = "Basil", UnitPrice = 20},
                new Ingredient{Name = "Lentil", UnitPrice = 40},
                new Ingredient{Name = "Butter", UnitPrice = 30},
                new Ingredient{Name = "Egg", UnitPrice = 25},
                new Ingredient{Name = "Cocoa powder", UnitPrice = 25},
                new Ingredient{Name = "Sugar", UnitPrice = 25},
                new Ingredient{Name = "Flour", UnitPrice = 20},
                new Ingredient{Name = "Salt", UnitPrice = 5},
            };
            foreach (Ingredient i in ingredient)
            {
                recipeDb.Ingredients.Add(i);
                recipeDb.SaveChanges();

                i.Id = i.Id;
            }


            var recipeIngredients1 = new List<RecipeIngredient>
            {
                new RecipeIngredient
                {
                    RecipeId = 1,
                    IngredientId = ingredient[0].Id,
                    Value = 1,
                    Measure = Measure.cup
                },
                new RecipeIngredient
                {
                    RecipeId = 1,
                    IngredientId = ingredient[1].Id,
                    Value = 3,
                    Measure = Measure.floz
                },
                new RecipeIngredient
                {
                    RecipeId = 1,
                    IngredientId = ingredient[2].Id,
                    Value = 2,
                    Measure = Measure.pt
                }
            };
            var recipe1 = new Recipe()
            {
                Name = "Tomato Soup",
                Description = "Add the onion and carrots and saute for 8-10 minutes, until tender. Add the garlic and cook for 1 minute. Add the tomatoes, tomato paste, basil, chicken stock, salt, and pepper and stir well. Bring the soup to a boil, lower the heat, and simmer, uncovered, for 30 minutes, until the tomatoes are very tender.",
                CategoryId = category[1].Id,
                Difficulty = Difficulty.Easy,
                RecipeIngredients = recipeIngredients1
            };
            recipeDb.Recipes.Add(recipe1);




            var recipeIngredients2 = new List<RecipeIngredient>
            {
                new RecipeIngredient
                {
                    RecipeId = 2,
                    IngredientId = ingredient[3].Id,
                    Value = 4,
                    Measure = Measure.tbs
                },
                new RecipeIngredient
                {
                    RecipeId = 2,
                    IngredientId = ingredient[4].Id,
                    Value = 2,
                    Measure = Measure.cup
                },
                new RecipeIngredient
                {
                    RecipeId = 2,
                    IngredientId = ingredient[5].Id,
                    Value = 3,
                    Measure = Measure.cup
                }
            };
            var recipe2 = new Recipe()
            {
                Name = "Vegetable Lasagna",
                Description = "Stir the ricotta cheese, 1/2 cup Parmesan cheese and eggs in a medium bowl and set it aside.  Season the beef as desired.  In a 3-quart saucepan over medium-high heat, cook the beef until it's well browned, stirring often to break up the meat. Pour off any fat. Stir the sauce in the saucepan.",
                CategoryId = category[2].Id,
                Difficulty = Difficulty.Hard,
                RecipeIngredients = recipeIngredients2
            };
            recipeDb.Recipes.Add(recipe2);



            var recipeIngredients3 = new List<RecipeIngredient>
            {
                new RecipeIngredient
                {
                    RecipeId = 3,
                    IngredientId = ingredient[6].Id,
                    Value = 2,
                    Measure = Measure.gill
                },
                new RecipeIngredient
                {
                    RecipeId = 3,
                    IngredientId = ingredient[7].Id,
                    Value = 3,
                    Measure = Measure.cup
                },
                new RecipeIngredient
                {
                    RecipeId = 3,
                    IngredientId = ingredient[8].Id,
                    Value = 1,
                    Measure = Measure.tsp
                }
            };
            var recipe3 = new Recipe()
            {
                Name = "Brownies",
                Description = "Preheat oven to 350 degrees F. Line a metal 9x9 pan with parchment paper. Pour melted butter into a large mixing bowl. Whisk in sugar by hand until smooth, 30 seconds. Add in eggs and vanilla extract. Whisk 1 minute. Whisk in melted chocolate until combined and smooth. Use a rubber spatula to stir in flour, cocoa powder, and salt until just combined. Stir in whole chocolate chips. Pour into prepared pan and smooth out. Bake in the preheated oven for 30 minutes.Let cool in pan 30 minutes before slicing.",
                CategoryId = category[0].Id,
                Difficulty = Difficulty.Intermediate,
                RecipeIngredients = recipeIngredients3
            };
            recipeDb.Recipes.Add(recipe3);



            var recipeIngredients4 = new List<RecipeIngredient>
            {
                new RecipeIngredient
                {
                    RecipeId = 4,
                    IngredientId = ingredient[9].Id,
                    Value = 3,
                    Measure = Measure.cup
                },
                new RecipeIngredient
                {
                    RecipeId = 4,
                    IngredientId = ingredient[10].Id,
                    Value = 6,
                    Measure = Measure.tbs
                },
                new RecipeIngredient
                {
                    RecipeId = 4,
                    IngredientId = ingredient[11].Id,
                    Value = 1,
                    Measure = Measure.pt
                }
            };
            var recipe4 = new Recipe()
            {
                Name = "Lentil soup",
                Description = "Preheat oven to 350 degrees F. Line a metal 9x9 pan with parchment paper. Pour melted butter into a large mixing bowl. Whisk in sugar by hand until smooth, 30 seconds. Add in eggs and vanilla extract. Whisk 1 minute. Whisk in melted chocolate until combined and smooth. Use a rubber spatula to stir in flour, cocoa powder, and salt until just combined. Stir in whole chocolate chips. Pour into prepared pan and smooth out. Bake in the preheated oven for 30 minutes.Let cool in pan 30 minutes before slicing.",
                CategoryId = category[1].Id,
                Difficulty = Difficulty.Easy,
                RecipeIngredients = recipeIngredients4
            };
            recipeDb.Recipes.Add(recipe4);

            recipeDb.SaveChanges();
        }
    }
}
