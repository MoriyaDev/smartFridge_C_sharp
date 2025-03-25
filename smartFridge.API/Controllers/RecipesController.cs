using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFridge.API.Models;
using SmartFridge.Core.DTOs;
using SmartFridge.Core.Model;
using SmartFridge.Core.Service;
using SmartFridge.Service.SmartFridge.Service;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartFridge.API.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;
        public RecipesController(IRecipeService recipeService, IMapper mapper)
        {
            _mapper = mapper;
            _recipeService = recipeService;
        }



        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return _recipeService.GetAll();
        }

        [HttpGet("bypro")]
        public async Task<IActionResult> GetRecipesByIngredients([FromQuery] string ingredients, [FromQuery] int fridgeId)
        {
            var recipes = await _recipeService.GetRecipesByProduct(ingredients, fridgeId);
            if (recipes == null || recipes.Count == 0)
            {
                return NotFound("לא נמצאו מתכונים עם המרכיבים הנבחרים");
            }
            return Ok(recipes);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public IActionResult AddRecipe([FromBody] RecipePostModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Products))
                {
                    return BadRequest("❌ כותרת ורשימת מוצרים הן חובה.");
                }
                var mappedRecipe = _mapper.Map<Recipe>(model);

                _recipeService.AddRecipe(mappedRecipe);
                return Ok(new { message = "✅ המתכון נוסף בהצלחה!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ שגיאת שרת: {ex.Message}");
            }
        }




    }
}