using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public int? AttributeValueId { get; set; } //no required!
        public string Value { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("AttributeId")]
        public Attribute Attribute { get; set; }

        [ForeignKey("AttributeValueId")]
        public AttributeValue AttributeValue { get; set; }
    }
}
