using OnlineShop.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs.Requests
{
    public class OrderRequestDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public List<OrderProductRequestDto> ProductQuantity { get; set; }
    }
}
