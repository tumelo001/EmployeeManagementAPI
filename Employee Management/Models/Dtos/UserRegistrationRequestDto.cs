using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models.Dtos
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
