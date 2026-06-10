using System.ComponentModel.DataAnnotations;

namespace StudentCourse.DTOs
{
    public class CreateCourseDto
    {
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [StringLength(30)]
        public string? Duration { get; set; }
    }
}