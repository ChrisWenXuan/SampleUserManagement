using System.ComponentModel.DataAnnotations;

namespace SampleUserManagement.Dtos
{
    public class EditUserDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

    }
}
