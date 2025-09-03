using System.ComponentModel.DataAnnotations;

namespace TechritizeAuthAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; }

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }  // Store hashed password

        [Required, MaxLength(20)]
        public string Role { get; set; } // "Client" or "Staff"
    }
}
