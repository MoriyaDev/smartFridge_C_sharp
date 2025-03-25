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
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    namespace SmartFridge.Service
    {
        public class FridgeService : IFridgeService
        {
            private readonly IFridgeRepository _fridgeRepository;

            public FridgeService(IFridgeRepository fridgeRepository)
            {
                _fridgeRepository = fridgeRepository;
            }

            public List<Fridge> GetAll()
            {
                return _fridgeRepository.GetAll();
            }

            public Fridge GetById(int id)
            {
                return _fridgeRepository.GetById(id);
            }

            public void Add(Fridge fridge)
            {
                fridge.Password = HashPassword(fridge.Password);
                _fridgeRepository.Add(fridge);
            }
            private string HashPassword(string password)
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(bytes);
                }
            }
            public void Update(Fridge fridge)
            {
                _fridgeRepository.Update(fridge);
            }
            public void Delete(int id)
            {
                _fridgeRepository.Delete(id);
            }
            //public Fridge Login(string username, string password)
            //{
            //    var fridge = _fridgeRepository.GetFridgeByName(username);

            //    if (fridge != null && fridge.Password == password)
            //    {
            //        return fridge;
            //    }

            //    throw new UnauthorizedAccessException("Invalid username or password");
            //}






        }
    }

}
