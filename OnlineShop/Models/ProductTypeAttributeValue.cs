namespace OnlineShop.Models
{
    public class ProductTypeAttributeValue
    {
        public int ProductTypeId { get; set; }
        public int AttributeValueId { get; set; }

        public ProductType ProductType { get; set; }
        public AttributeValue AttributeValue { get; set; }
    }
}
