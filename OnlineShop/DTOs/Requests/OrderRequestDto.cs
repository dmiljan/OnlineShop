using OnlineShop.Models;

namespace OnlineShop.DTOs.Requests
{
    public class OrderRequestDto
    {
        public string UserId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderProductRequestDto> ProductQuantity { get; set; }
    }
}
