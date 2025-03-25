using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartFridge.Core.Model;

namespace SmartFridge.Core.Service
{
    public interface INoteService
    {
        public List<Note> GetAll();
        public Note GetById(int id);
        public void Add(Note note);
        public void Update(Note note);
        public void Delete(int id);
        public IEnumerable<Note> GetNotesByFridgeId(int fridgeId);



    }

}
