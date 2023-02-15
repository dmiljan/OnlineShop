using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Responses;
using OnlineShop.Interfaces;
using Attribute = OnlineShop.Models.Attribute;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private IAttribute _attributeService;
        public IMapper _mapper;

        public AttributeController(IAttribute attributeService, IMapper mapper)
        {
            _attributeService = attributeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var attribute = await _attributeService.GetAttributes();
            return Ok(_mapper.Map<List<AttributeResponseDto>>(attribute));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(int id)
        {
            var attribute = await _attributeService.GetAttributeById(id);
            if (attribute != null)
            {
                return Ok(_mapper.Map<AttributeResponseDto>(attribute));
            }
            return NotFound($"Attribute with Id: {id} was not found");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                await _attributeService.AddAttribute(attribute);
                var _attribute = _mapper.Map<AttributeResponseDto>(attribute);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + _attribute.Id, _attribute);
            }
            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> Update(Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                await _attributeService.UpdateAttribute(attribute);
                return Ok(_mapper.Map<AttributeResponseDto>(attribute));
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var attribute = await _attributeService.GetAttributeById(id);
            if (attribute != null)
            {
                await _attributeService.DeleteAttribute(attribute);
                return Ok();
            }
            return NotFound($"Attribute with Id: {id} was not found");
        }
    }
}
