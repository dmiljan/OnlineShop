using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class AttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int AttributeId { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Label { get; set; }
        
        [ForeignKey("AttributeId")]
        public Attribute Attribute { get; set; }

        public ICollection<ProductTypeAttributeValue> ProductTypes { get; set; } //many-to-many
    }
}
