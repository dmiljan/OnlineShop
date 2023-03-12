using OnlineShop.Models;

namespace OnlineShop.DTOs.Responses
{
    public class ProductResponseDto
    {
        public Product Product { get; set; }
        public string ProductType { get; set; }
        public IEnumerable<AttributeForDto> Attributes { get; set; }
        public IEnumerable<string> Images { get; set; }
    }

    public class AttributeForDto
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string InputType { get; set; }
    }
}
