using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class UserProductSave
    {
        [Required]
        public string UserId { get; set; }  //string!
        public int ProductId { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
