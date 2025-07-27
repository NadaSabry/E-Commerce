using E_Commerce.Core.Models;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly productService _productService;

        public ProductController(productService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var product  = await _productService.GetAllProduct();
            return Ok(product);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByID(id);
                if (product == null)
                {
                    var errorResponse = new { code = -50, message = $"No product found with id = {id}"  };
                    return BadRequest(errorResponse);
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                var errorResponse = new { code = -5, message = "General Error" };
                return BadRequest(errorResponse);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                _productService.AddProduct(product);
                return Created();
            }
            catch (Exception ex)
            {
                var errorResponse = new { code=-5 , message= "General Error"};
                return BadRequest(errorResponse);
            }
        }
        [HttpPut]
        public async Task<IActionResult> EditProduct(Product product)
        {
            try
            {
                _productService.UpdateProduct(product);
                return Ok("updated sucessfuly");
            }
            catch (Exception ex)
            {
                var errorResponse = new { code = -5, message = "General Error" };
                return BadRequest(errorResponse);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var deletedproduct = await _productService.GetProductByID(id);
                if (deletedproduct == null)
                {
                    return NotFound(new { code = -404, message = $"No product found with id = {id}" });
                }
                _productService.DeleteProduct(deletedproduct);
                return Ok(deletedproduct);
            }
            catch (Exception ex)
            {
                var errorResponse = new { code = -5, message = "General Error" };
                return BadRequest(errorResponse);
            }
        }
    }
}
