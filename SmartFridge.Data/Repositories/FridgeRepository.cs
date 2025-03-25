using Microsoft.EntityFrameworkCore;
using SmartFridge.Core.Model;
using SmartFridge.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Data.Repositories
{
    public class FridgeRepository : IFridgeRepository
    {
        private readonly DataContext _context;

        public FridgeRepository(DataContext context)
        {
            _context = context;
        }

        public List<Fridge> GetAll()
        {
            try
            {
                return _context.Fridges.Include(f => f.Products).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת כל המקררים.", ex);
            }
        }

        public Fridge GetById(int id)
        {
            try
            {
                var fridge = _context.Fridges
                    .Include(f => f.Products)
                    .Include(f => f.Notes)
                    .FirstOrDefault(f => f.Id == id);

                if (fridge == null)
                {
                    throw new KeyNotFoundException($"לא נמצא מקרר עם מזהה {id}.");
                }

                return fridge;
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת מקרר.", ex);
            }
        }

        public void Add(Fridge fridge)
        {
            _context.Fridges.Add(fridge);
            _context.SaveChanges();
        }

        public void Update(Fridge fridge)
        {
            if (fridge == null)
            {
                throw new ArgumentNullException(nameof(fridge), "המקרר לעדכון לא יכול להיות null");
            }

            try
            {
                var existingFridge = _context.Fridges.Find(fridge.Id);
                if (existingFridge == null)
                {
                    throw new KeyNotFoundException($"לא נמצא מקרר עם מזהה {fridge.Id}.");
                }

                existingFridge.Name = fridge.Name;
                existingFridge.Password = fridge.Password;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בעדכון המקרר.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var fridge = _context.Fridges.Find(id);
                if (fridge == null)
                {
                    throw new KeyNotFoundException($"לא נמצא מקרר עם מזהה {id}.");
                }

                _context.Fridges.Remove(fridge);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בהסרת מקרר.", ex);
            }
        }

        public Fridge GetFridgeByName(string Fname)
        {
            return _context.Fridges.FirstOrDefault(f => f.Name == Fname);
        }


    }



}
