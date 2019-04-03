﻿using System.Collections.Generic;
using Recipes.Bo;

namespace Recipes.Dto
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}