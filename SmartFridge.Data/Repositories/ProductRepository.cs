using SmartFridge.Core.Model;
using SmartFridge.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Product> GetProductsByFridgeId(int fridgeId)
        {
            return _context.Products.Where(p => p.FridgeId == fridgeId).ToList();
        }
        public void Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "המוצר לא יכול להיות null");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "המוצר לעדכון לא יכול להיות null");
            }

            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"לא נמצא מוצר עם מזהה {product.Id}");
            }

            existingProduct.Name = product.Name;
            existingProduct.FridgeId = product.FridgeId;
            existingProduct.CategoryID = product.CategoryID;
            existingProduct.Image = product.Image;

            existingProduct.ExpiryDate = product.ExpiryDate;
            existingProduct.Location = product.Location;

            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"לא נמצא מוצר עם מזהה {id}");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }

}
