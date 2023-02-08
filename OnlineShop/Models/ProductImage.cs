using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string ImagePath { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
