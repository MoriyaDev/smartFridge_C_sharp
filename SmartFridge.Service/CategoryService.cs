using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Service
{
    using global::SmartFridge.Core.Model;
    using global::SmartFridge.Core.Repositories;
    using global::SmartFridge.Core.Service;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace SmartFridge.Service
    {
        public class CategoryService : ICategoryService
        {
            private readonly ICategoryRepository _categoryRepository;
            public CategoryService(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }
            public List<Category> GetAll()
            {
                return _categoryRepository.GetAll();
            }
            public string GetCategoryName(int categoryId)
            {
                return _categoryRepository.GetCategoryNameById(categoryId);
            }

        }
    }

}
