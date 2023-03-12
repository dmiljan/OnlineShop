namespace OnlineShop.DTOs.Responses
{
    public class AttributeValueForProductTypeResponseDto
    {
        public string AttributeName { get; set; }
        public IEnumerable<AttributeValuesForDto> Values { get; set; }
    }
    public class AttributeValuesForDto
    {
        public int AttributeValueId { get; set; }
        public string Value { get; set; }
    }
}
