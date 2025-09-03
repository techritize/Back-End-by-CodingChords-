using System.ComponentModel.DataAnnotations;

namespace TechritizeAuthAPI.Models.Requests
{
    public class ResetPasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }  // 2FA OTP Code

        [Required, MinLength(6)]
        public string NewPassword { get; set; }
    }
}
