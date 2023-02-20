using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Responses;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeValueController : ControllerBase
    {
        public IAttributeValue _attributeValueService;
        public IMapper _mapper;

        public AttributeValueController(IAttributeValue attributeValueService, IMapper mapper)
        {
            _attributeValueService = attributeValueService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AttributeValueRequestDto attributeValueRequestDto)
        {
            var _attributeValue = _mapper.Map<AttributeValue>(attributeValueRequestDto);

            await _attributeValueService.AddAttributeValue(_attributeValue);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + _attributeValue.Id, _attributeValue);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(int id)
        {
            var attributeValue = await _attributeValueService.GetAttributeValueById(id);
            if(attributeValue != null)
            {

                return Ok(_mapper.Map<AttributeValueResponseDto>(attributeValue));
            }
            return NotFound($"Attribute value with Id: {id} was not found");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var attributeValues = await _attributeValueService.GetAttributeValues();
            return Ok(_mapper.Map<List<AttributeValueResponseDto>>(attributeValues));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOnlyLabel(int id, [FromBody] string label)
        {
            var attributeValue = await _attributeValueService.UpdateAttributeValueOnlyLabel(id, label);

            return Ok(attributeValue);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var attribteValue = await _attributeValueService.GetAttributeValueById(id);
            if(attribteValue != null)
            {
                await _attributeValueService.DeleteAttributeValue(attribteValue);
                return Ok();
            }
            return NotFound($"Attribute value with Id: {id} was not found");
        }
    }
}
