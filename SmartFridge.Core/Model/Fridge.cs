using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.Model
{
    public class Fridge
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Product> Products { get; set; }
        public List<Note> Notes { get; set; }
        public string Role { get; set; } = "User"; 


    }
}
