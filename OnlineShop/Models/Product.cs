using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Model { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        public ICollection<OrderProduct> Orders { get; set; } //many-to-many
        public ICollection<UserProductSave> Users { get; set; } //many-to-many
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
    }
}
