using OnlineShop.Models;

namespace OnlineShop.DTOs.Responses
{
    public class OrderResponseDto
    {
        public Order Order { get; internal set; }
        public UserForDto UserDetails { get; set; }
        public IEnumerable<ProductForDto> Products { get; set; }
    }

    public class UserForDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class ProductForDto
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public int QuantityInCart { get; set; }
        public float Price { get; set; }
        public string ProductType { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
