using System.ComponentModel.DataAnnotations;

namespace ProjectFor7COMm.DTOs
{
    public class PasswordResetDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
