using SmartFridge.Core.Model;

namespace SmartFridge.API.Models
{
    public class ProductPostModel
    {

        public string Name { get; set; } 
        public int FridgeId { get; set; }
        public int CategoryID { get; set; }
        public string Image { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
