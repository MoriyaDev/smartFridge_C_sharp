using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFridge.API.Models;
using SmartFridge.Core.Model;
using SmartFridge.Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartFridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgesController : ControllerBase
    {
        private readonly IFridgeService _fridgeService;
        private readonly IMapper _mapper;

        public FridgesController(IFridgeService fridgeService, IMapper mapper)
        {
            _fridgeService = fridgeService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Fridge>> Get()
        {
            var fridges = _fridgeService.GetAll();
            return Ok(fridges); 
        }


        [HttpGet("{id}")]
        public ActionResult<Fridge> Get(int id)
        {
            var fridge = _fridgeService.GetById(id);
            if (fridge == null)
            {
                return NotFound($"לא נמצא מקרר עם מזהה {id}");
            }
            return Ok(fridge);
        }


        [HttpPost]
        public ActionResult Post([FromBody] FridgePostModel fridge)
        {
            _fridgeService.Add(_mapper.Map<Fridge>(fridge));
            return CreatedAtAction(nameof(Get), fridge);

        }

    }
}
