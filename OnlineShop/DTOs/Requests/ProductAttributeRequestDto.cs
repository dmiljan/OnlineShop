using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs.Requests
{
    public class ProductAttributeRequestDto
    {
        [Required]
        public int AttributeId { get; set; }
        public int? AttributeValueId { get; set; } //no required
        [Required]
        public string Value { get; set; }
    }
}
