using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.Model
{


    public class Recipe
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int MissedIngredientCount { get; set; }
        public int UsedIngredientCount { get; set; }
        public List<Product> MissedIngredients { get; set; } = new List<Product>();
        public List<Product> UsedIngredients { get; set; } = new List<Product>();
        public string Products { get; set; } = string.Empty;
        public string MissedPro { get; set; } = string.Empty;
        public string UsedPro { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
