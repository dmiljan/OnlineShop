using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Key { get; set; }

        public ICollection<ProductTypeAttribute> Attributes { get; set; } //many-to-many
        public ICollection<ProductTypeAttributeValue> AttributeValues { get; set; } //many-to-many
    }
}
