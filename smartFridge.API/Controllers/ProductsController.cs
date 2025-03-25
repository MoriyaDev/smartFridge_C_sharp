using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFridge.API.Models;
using SmartFridge.Core.Model;
using SmartFridge.Core.Service;
using SmartFridge.Service.SmartFridge.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartFridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IFridgeService _fridgeService;


        public ProductsController(IProductService productService,
            IMapper mapper
            , IFridgeService fridgeService)
        {
            _productService = productService;
            _mapper = mapper;
            _fridgeService = fridgeService;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("fridge/{fridgeId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByFridgeId(int fridgeId)
        {
            var products = _productService.GetProductsByFridgeId(fridgeId);

            if (products == null || !products.Any())
            {
                return NotFound($"לא נמצאו מוצרים עבור מקרר עם מזהה {fridgeId}");
            }

            return Ok(products);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductPostModel product)
        {
            var mappedProduct = _mapper.Map<Product>(product);
            var fridge = _fridgeService.GetById(product.FridgeId);

            _productService.Add(mappedProduct);
            return CreatedAtAction(nameof(Get), new { id = mappedProduct.Id }, mappedProduct);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
            {
                return BadRequest("ה-ID של המוצר לא תואם");
            }

            var existingProduct = _productService.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productService.Update(product);

            return Ok(product); ;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.Delete(id);
            return NoContent();
        }
    }

}
