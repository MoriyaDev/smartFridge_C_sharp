using SmartFridge.Core.Repositories;
using System;
using System.Collections.Generic;
using SmartFridge.Core.Model;
using SmartFridge.Core.Service;

namespace SmartFridge.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        public List<Note> GetAll()
        {
            return _noteRepository.GetAll();
        }
        public Note GetById(int id) 
        { 
            return _noteRepository.GetById(id);
        }
        public IEnumerable<Note> GetNotesByFridgeId(int fridgeId)
        {
            return _noteRepository.GetNotesByFridgeId(fridgeId);
        }
        public void Add(Note note)
        {
            Console.WriteLine($"📌 מתקבלת בקשת הוספה: {note.Text}, {note.CreatedDate}");
            _noteRepository.Add(note);
        }
        public void Update(Note note)
        {
            _noteRepository.Update(note);
        }
        public void Delete(int id)
        {
            _noteRepository.Delete(id);
        }

    
    }
}
