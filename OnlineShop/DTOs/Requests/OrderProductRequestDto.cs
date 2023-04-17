using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs.Requests
{
    public class OrderProductRequestDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
