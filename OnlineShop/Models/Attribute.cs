using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ValueType { get; set; }
        [Required]
        public string InputType { get; set; }

        public ICollection<ProductTypeAttribute> ProductTypes { get; set; } //many-to-many
        public virtual ICollection<AttributeValue> AttributeValue { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
    }
}
