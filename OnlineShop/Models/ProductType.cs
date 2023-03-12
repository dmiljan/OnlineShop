using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ProductTypeAttribute> Attributes { get; set; } //many-to-many
        public ICollection<ProductTypeAttributeValue> AttributeValues { get; set; } //many-to-many
        public virtual ICollection<Product> Product { get; set; }
    }
}
