using System.ComponentModel.DataAnnotations;

namespace TechritizeAuthAPI.Models.Requests
{
    public class SignUpRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; }

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, MinLength(6)]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression("^(Client|Staff)$", ErrorMessage = "Role must be either 'Client' or 'Staff'")]
        public string Role { get; set; }
    }
}
