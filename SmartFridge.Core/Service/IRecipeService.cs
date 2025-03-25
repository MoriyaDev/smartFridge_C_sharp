using SmartFridge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.Service
{
    public interface IRecipeService
    {
        public List<Recipe> GetAll();
        Task<List<Recipe>> GetRecipesByIngredientsAsync(string ingredients, int number = 2);
        Task<List<Recipe>> GetRecipesByProduct(string ingredients, int fridgeId);
        void AddRecipe(Recipe model);

        Task<string> GetRecipeInstructionsAsync(int recipeId);





    }
}

