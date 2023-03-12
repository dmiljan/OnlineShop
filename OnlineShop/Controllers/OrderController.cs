using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Requests.Queries;
using OnlineShop.DTOs.Responses;
using OnlineShop.DTOs.Responses.Pagination;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrder _orderService;
        public IMapper _mapper;

        public OrderController(IOrder orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequestDto order)
        {
            var _order = await _orderService.AddOrder(order);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + _order.Id, _order);
        }

        [HttpPatch]
        public async Task<IActionResult> CompleteOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CompleteOrder(order);

                return Ok(order);
            }                                                                                                                                                                                                                                                                                                                                                                                                                                            

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetAllOrderFilter filter, [FromQuery] PaginationQuery paginationQuery)
        {
            var orders = await _orderService.GetOrders(filter, paginationQuery);
            var paginationResponse = new PagedResponse<OrderResponseDto>(orders, paginationQuery.PageNumber, paginationQuery.PageSize);
            
            return Ok(paginationResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(int id)
        {
            var order = await _orderService.GetOrderWithAllProducts(id);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound($"Order with Id: {id} was not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetails = await _orderService.GetOrderWithAllProducts(id);
            var order = orderDetails.Order;

            if (order != null)
            {
                await _orderService.DeleteOrder(order);
                return Ok();
            }
            return NotFound($"Order with Id: {id} was not found");
        }
    }
}
