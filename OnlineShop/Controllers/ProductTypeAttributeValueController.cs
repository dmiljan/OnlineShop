using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Responses;
using OnlineShop.Interfaces;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeAttributeValueController : ControllerBase
    {
        public IProductTypeAttributeValue _productTypeAttributeValueService;
        public IMapper _mapper;

        public ProductTypeAttributeValueController(IProductTypeAttributeValue productTypeAttributeValueService, IMapper mapper)
        {
            _productTypeAttributeValueService = productTypeAttributeValueService;
            _mapper = mapper;
        }

        //Add attribute values to product type
        [HttpPost]
        public async Task<IActionResult> Create(List<ProductTypeAttributeValue> listProductTypeAttributeValue)
        {
            if (ModelState.IsValid)
            {
                await _productTypeAttributeValueService.AddAtributesValueToProductType(listProductTypeAttributeValue);
                return Ok(listProductTypeAttributeValue.ToList());
            }
            return BadRequest();
        }

        [HttpDelete("{productTypeId}&{attributeValueId}")]
        public async Task<IActionResult> Delete(int productTypeId, int attributeValueId)
        {
            var productTypeAttributeValue = await _productTypeAttributeValueService.GetPoductTypeAttributeValueById(productTypeId, attributeValueId);

            if (productTypeAttributeValue != null)
            {
                await _productTypeAttributeValueService.DeleteAssignedAttributeValueToProductType(productTypeAttributeValue);
                return Ok();
            }
            return NotFound($"ProductType with Id: {productTypeId} and/or Attribute Value with Id: {attributeValueId} were/was not found in ProductTypeAttributeValue table.");
        }

        /// <summary>
        /// Get all attribute values for one product type.
        /// </summary>
        [HttpGet("{productTypeId}")]
        public async Task<IActionResult> GetAll(int productTypeId)
        {
            var ProductTypeAttributeValues = await _productTypeAttributeValueService.GetAllAttributeValuesForProductType(productTypeId);
            return Ok(_mapper.Map<List<ProductTypeAttributeValueResponseDto>>(ProductTypeAttributeValues));
        }
    }
}
