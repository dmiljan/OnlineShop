using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs.Requests
{
    public class AttributeValueRequestDto
    {
        [Required]
        public int AttributeId { get; set; }
        [Required]
        public string Label { get; set; }
    }
}
