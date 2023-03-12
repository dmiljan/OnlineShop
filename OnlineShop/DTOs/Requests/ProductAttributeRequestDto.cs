namespace OnlineShop.DTOs.Requests
{
    public class ProductAttributeRequestDto
    {
        public int AttributeId { get; set; }
        public int? AttributeValueId { get; set; } //no required
        public string Value { get; set; }
    }
}
