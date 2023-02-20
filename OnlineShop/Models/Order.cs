using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } //string, no required
        [Required]
        public bool Processed { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public float TotalPrice { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<OrderProduct> Products { get; set; } //many-to-many
    }
}
