using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs.Requests
{
    public class RegisterRequestDto
    {

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        [MinLength(5)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmaton password did not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
