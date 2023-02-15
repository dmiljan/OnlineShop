using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Responses;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeAttributeController : ControllerBase
    {
        public IProductTypeAttribute _productTypeAttributeService;
        public IMapper _mapper;

        public ProductTypeAttributeController(IProductTypeAttribute productTypeAttributeService, IMapper mapper)
        {
            _productTypeAttributeService = productTypeAttributeService;
            _mapper = mapper;
        }

        //Add attributes to product type
        [HttpPost]
        public async Task<IActionResult> Create(List<ProductTypeAttribute> listProductTypeAttribute)
        {
            if (ModelState.IsValid)
            {
                await _productTypeAttributeService.AddAtributesToProductType(listProductTypeAttribute);
                return Ok(listProductTypeAttribute.ToList());
            }
            return BadRequest();
        }

        [HttpDelete("{productTypeId}&{attributeId}")]
        public async Task<IActionResult> Delete(int productTypeId, int attributeId)
        {
            var productTypeAttribute = await _productTypeAttributeService.GetPoductTypeAttributeById(productTypeId, attributeId);

            if(productTypeAttribute != null)
            {
                await _productTypeAttributeService.DeleteAssignedAttributeToProductType(productTypeAttribute);
                return Ok();
            }
            return NotFound($"ProductType with Id: {productTypeId} and/or Attribute with Id: {attributeId} were/was not found in ProductTypeAttribute table.");
        }

        /// <summary>
        /// Get all attributes for one product type
        /// </summary>
        [HttpGet("{productTypeId}")]
        public async Task<IActionResult> GetAll (int productTypeId)
        {
            var ProductTypeAttributes = await _productTypeAttributeService.GetAllAttributesForProductType(productTypeId);
            return Ok(_mapper.Map<List<ProductTypeAttributeResponseDto>>(ProductTypeAttributes));
        }
    }
}
