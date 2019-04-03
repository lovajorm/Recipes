﻿using System.Collections.Generic;

namespace Recipes.Bo
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int RecipeId { get; set; }
    }
}