using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Requests.Queries;
using OnlineShop.DTOs.Responses;
using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IOrder
    {
        Task<Order> AddOrder(OrderRequestDto order);
        Task CompleteOrder(int orderId);
        Task DeleteOrder(Order order);
        Task<IEnumerable<OrderResponseDto>> GetOrders(GetAllOrderFilter filter = null, PaginationQuery paginationQuery = null);
        Task<OrderResponseDto> GetOrderWithAllProducts(int id);
    }
}
