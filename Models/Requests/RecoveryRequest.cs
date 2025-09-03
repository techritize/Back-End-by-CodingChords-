using System.ComponentModel.DataAnnotations;

namespace TechritizeAuthAPI.Models.Requests
{
    public class RecoveryRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
