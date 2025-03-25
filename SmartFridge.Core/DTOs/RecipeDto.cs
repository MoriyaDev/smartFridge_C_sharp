using SmartFridge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.DTOs
{
    public class RecipeDto
    {
        public string Title { get; set; } = string.Empty;
        public string Products { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
    }
}