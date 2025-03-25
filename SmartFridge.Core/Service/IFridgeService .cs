using SmartFridge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Core.Service
{
    public interface IFridgeService
    {
        public List<Fridge> GetAll();
        public Fridge GetById(int id);
        public void Add(Fridge fridge);
        public void Update(Fridge fridge);
        public void Delete(int id);
        //public Fridge Login(string Fname, string password);


    }
}
