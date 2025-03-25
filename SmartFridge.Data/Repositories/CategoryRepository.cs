using SmartFridge.Core.Model;
using SmartFridge.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Data.Repositories
{
    public class CategoryRepository  : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        
        public string GetCategoryNameById(int categoryId)
        {
            return _context.Categories
                           .Where(c => c.Id == categoryId)
                           .Select(c => c.Name)
                           .FirstOrDefault() ?? "קטגוריה לא ידועה";
        }


    }
}
