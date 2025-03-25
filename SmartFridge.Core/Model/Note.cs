using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.Model
{
    public class Note
    {
        public int Id { get; set; } 
        public int FridgeId { get; set; } 
        public string Text { get; set; } = string.Empty; 
        public string Type { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } 
        public bool IsResolved { get; set; } 
    }
}
