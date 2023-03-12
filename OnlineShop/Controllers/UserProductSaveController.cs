using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Requests.Queries;
using OnlineShop.DTOs.Responses;
using OnlineShop.DTOs.Responses.Pagination;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductSaveController : ControllerBase
    {
        public IUserProductSave _userProductSave;
        public IMapper _mapper;

        public UserProductSaveController(IUserProductSave userProductSave, IMapper mapper)
        {
            _userProductSave = userProductSave;
            _mapper = mapper;
        }

        /// <summary>
        /// User can save the product in his list of favorite (saved) products.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(UserProductSave userProductSave)
        {
            if (ModelState.IsValid)
            {
                await _userProductSave.UserSaveProduct(userProductSave);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + userProductSave.ProductId, userProductSave);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get all saved (favorite) products for one user.
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> Index(string userId, [FromQuery] PaginationQuery paginationQuery)
        {
            var savedProductsOfUser = await _userProductSave.GetAllSavedProducts(userId, paginationQuery);
            var paginationResponse = new PagedResponse<ProductResponseDto>(savedProductsOfUser, paginationQuery.PageNumber, paginationQuery.PageSize);

            return Ok(paginationResponse);
        }

        [HttpDelete("{userId}&{productId}")]
        public async Task<IActionResult> Delete(string userId, int productId)
        {
            var userProductSave = new UserProductSave()
            {
                UserId = userId,
                ProductId = productId
            };
            await _userProductSave.DeleteSavedProduct(userProductSave);

            return Ok();
        }
    }
}
