using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DTOs.Responses
{
    public class AttributeValueResponseDto
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public string Label { get; set; }
    }
}
