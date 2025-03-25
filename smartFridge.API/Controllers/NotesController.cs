using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFridge.Core.Model;
using SmartFridge.Core.Service;
using SmartFridge.Service;
using SmartFridge.Service.SmartFridge.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartFridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {

        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("byFridge/{fridgeId}")]
        public ActionResult<IEnumerable<Note>> GetNotesByFridgeId(int fridgeId)
        {
            var notes = _noteService.GetNotesByFridgeId(fridgeId);

            if (notes == null || !notes.Any())
            {
                return NotFound($"לא נמצאו מוצרים עבור מקרר עם מזהה {fridgeId}");
            }

            return Ok(notes);
        }

        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return _noteService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            var note = _noteService.GetById(id);
            if (note == null)
            {
                return NotFound($"Note with ID {id} not found.");
            }
            return Ok(note);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
         
            _noteService.Add(note); 
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note); 
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Note note)
        {
            _noteService.Update(note);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _noteService.Delete(id);
        }
    }
}
