namespace OnlineShop.Models
{
    public class ProductTypeAttribute
    {
        public int ProductTypeId { get; set; }
        public int AttributeId { get; set; }
        public bool IsMandatory { get; set; }

        public ProductType ProductType { get; set; }
        public Attribute Attribute { get; set; }
    }
}
