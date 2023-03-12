using Microsoft.AspNetCore.Mvc;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        public IProductImage _prodiuctImageService;

        public ProductImageController(IProductImage prodiuctImageService)
        {
            _prodiuctImageService = prodiuctImageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                await _prodiuctImageService.AddImage(productImage);
                return Ok(productImage);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _prodiuctImageService.GetImageById(id);
            if(image != null)
            {
                await _prodiuctImageService.DeleteImage(image);
                return Ok();
            }
            return NotFound($"Image with Id: {id} was not found");
        }
    }
}
