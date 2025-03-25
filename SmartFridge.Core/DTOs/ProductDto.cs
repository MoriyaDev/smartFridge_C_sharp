using SmartFridge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.DTOs
{
    public class ProductDto
    {

        public string Name { get; set; } = string.Empty;
        public int FridgeId { get; set; }
        public int CategoryID { get; set; }
        public string Image { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public string Location { get; set; } = string.Empty;

    }
}
