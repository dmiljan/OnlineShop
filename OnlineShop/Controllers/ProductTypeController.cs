using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Responses;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        public IProductType _productTypeService;
        public IMapper _mapper;

        public ProductTypeController(IProductType productTypeService, IMapper mapper)
        {
            _productTypeService = productTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productTypes = await _productTypeService.GetProductTypes();
            return Ok(_mapper.Map<List<ProductTypeResponseDto>>(productTypes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(int id)
        {
            var productType = await _productTypeService.GetProductTypeById(id);
            if(productType != null)
            {
                return Ok(_mapper.Map<ProductTypeResponseDto>(productType));
            }
            return NotFound($"Product type with Id: {id} was not found");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                await _productTypeService.AddProductType(productType);
                var _productType = _mapper.Map<ProductTypeResponseDto>(productType);

                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + _productType.Id, _productType);
            }
           return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateName(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                await _productTypeService.UpdateNameOfProductType(productType);

                return Ok(_mapper.Map<ProductTypeResponseDto>(productType));
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productType = await _productTypeService.GetProductTypeById(id);
            if(productType != null)
            {
                await _productTypeService.DeleteProductType(productType);
                return Ok();
            }
            return NotFound($"Product Type with Id: {id} was not found");
        }
    }
}
