using System.ComponentModel.DataAnnotations;

namespace SportsManager.Api.Models
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
    }

    public class RegistrationResult
    {
        public bool Success { get; set; }
        public int UserId { get; set; }
        public string ErrorMessage { get; set; }

    }
}
