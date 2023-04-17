using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs.Requests
{
    public class ProductRequestDto
    {
        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public List<ProductAttributeRequestDto> ProductAttribute { get; set; }
    }
}
