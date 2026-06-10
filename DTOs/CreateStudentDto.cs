using System.ComponentModel.DataAnnotations;

namespace StudentCourse.DTOs
{
    public class CreateStudentDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(18, 100)]
        public int? Age { get; set; }
    }
}