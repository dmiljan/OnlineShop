using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs.Requests
{
    public class RegisterRequestDto
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        [MinLength(5)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmaton password did not match")]
        public string ConfirmPassword { get; set; }
    }
}
