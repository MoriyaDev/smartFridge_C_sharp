using SmartFridge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.Repositories
{
    public interface IRecipeRepository
    {
        public List<Recipe> GetAll();
        void AddRecipe(Recipe recipe);




    }
}
