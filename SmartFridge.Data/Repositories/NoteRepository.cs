using SmartFridge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using SmartFridge.Core.Model;
using SmartFridge.Core.Repositories;

namespace SmartFridge.Data.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly DataContext _context;
        public NoteRepository(DataContext context)
        {
            _context = context ;
        }
        public IEnumerable<Note> GetNotesByFridgeId(int fridgeId)
        {
            return _context.Notes.Where(n => n.FridgeId == fridgeId).ToList();
        }
        public List<Note> GetAll()
        {
            try
            {
                return _context.Notes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת כל ההערות.", ex);
            }
        }
        public Note GetById(int id)
        {
            try
            {
                var note = _context.Notes.Find(id);
                if (note == null)
                {
                    throw new KeyNotFoundException($"לא נמצאה הערה עם מזהה {id}.");
                }
                return note;
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת הערה.", ex);
            }
        }
        public void Add(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "ההערה לא יכולה להיות null");
            }

            try
            {
                _context.Notes.Add(note);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בהוספת הערה חדשה.", ex);
            }
        }
        public void Update(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "ההערה לעדכון לא יכולה להיות null");
            }

            try
            {
                var existingNote = _context.Notes.Find(note.Id);
                if (existingNote == null)
                {
                    throw new KeyNotFoundException($"לא נמצאה הערה עם מזהה {note.Id}.");
                }

                existingNote.Text = note.Text;
                existingNote.FridgeId = note.FridgeId;
                existingNote.Type = note.Type;
                existingNote.IsResolved = note.IsResolved;
                existingNote.CreatedDate = DateTime.UtcNow;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בעדכון הערה.", ex);
            }
        }
        public void Delete(int id)
        {
            try
            {
                var note = _context.Notes.Find(id);
                if (note == null)
                {
                    throw new KeyNotFoundException($"לא נמצאה הערה עם מזהה {id}.");
                }

                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בהסרת הערה.", ex);
            }
        }

       
    }

}
