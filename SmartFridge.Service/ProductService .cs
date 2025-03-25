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
        public class ProductService : IProductService
        {
            private readonly IProductRepository _productRepository;

            public ProductService(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public List<Product> GetAll()
            {
                return _productRepository.GetAll();
            }

            public Product GetById(int id)
            {
                return _productRepository.GetById(id);
            }

            public IEnumerable<Product> GetProductsByFridgeId(int fridgeId)
            {
                return _productRepository.GetProductsByFridgeId(fridgeId);
            }

            public void Add(Product product)
            {
                _productRepository.Add(product);
                //_productRepository.SaveChanges(); // ← שומר את השינויים במסד הנתונים

            }

            public void Update(Product product)
            {
                _productRepository.Update(product);
            }

            public void Delete(int id)
            {
                _productRepository.Delete(id);
            }
        }

    }

}
