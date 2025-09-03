using System;
using System.ComponentModel.DataAnnotations;

namespace TechritizeAuthAPI.Models
{
    public class TwoFactorCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }   // User email tied to the code

        [Required]
        public string Code { get; set; }    // Random generated code

        [Required]
        public DateTime ExpiryTime { get; set; }  // Expiration (1 min)
    }
}
