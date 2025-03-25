using Microsoft.EntityFrameworkCore;
using SmartFridge.Core.Model;
using SmartFridge.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Data.Repositories
{
    public class RecipeRepository  : IRecipeRepository
    {
        private readonly DataContext _context;

        public RecipeRepository(DataContext context)
        {
            _context = context;
        }
        public List<Recipe> GetAll()
        {
            return _context.Recipes?.ToList() ?? new List<Recipe>();
        }
        public void AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }


    }
}
