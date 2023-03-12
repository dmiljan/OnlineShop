using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Requests.Queries;
using OnlineShop.DTOs.Responses;
using OnlineShop.DTOs.Responses.Pagination;
using OnlineShop.Interfaces;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProduct _productService;
        public IMapper _mapper;

        public ProductController(IProduct productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestDto productRequestDto)
        {
            var product = await _productService.AddProduct(productRequestDto);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + product.Id, product);
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetAllProductFilter filter, [FromQuery] PaginationQuery paginationQuery)
        {
            var product = await _productService.GetProducts(filter, paginationQuery);
            var paginationResponse = new PagedResponse<ProductResponseDto>(product, paginationQuery.PageNumber, paginationQuery.PageSize);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound($"Product with Id: {id} was not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _productService.GetProductById(id);
            var product = productDetails.Product;

            if (product != null)
            {
                await _productService.DeleteProduct(product);
                return Ok();
            }
            return NotFound($"Product with Id: {id} was not found");
        }

        [HttpPatch]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProduct(product);

                //return Ok(_mapper.Map<ProductTypeResponseDto>(product));
                return Ok(product);
            }
            return BadRequest();
        }

        //SEARCH
    }
}
