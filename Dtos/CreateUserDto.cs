using System.ComponentModel.DataAnnotations;

namespace SampleUserManagement.Dtos
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(255)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }
    }
}
