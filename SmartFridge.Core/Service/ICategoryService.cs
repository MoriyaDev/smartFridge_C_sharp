using SmartFridge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.Service
{
    public interface ICategoryService
    {
        public List<Category> GetAll();
        public string GetCategoryName(int categoryId);


    }
}
