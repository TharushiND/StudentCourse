using System.ComponentModel.DataAnnotations;

namespace StudentCourse.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; }

        [Range(18, 100)]
        public int? Age { get; set; }
        public ICollection<StudentCourseMapping> StudentCourses { get; set; }
         = new List<StudentCourseMapping>();
    }
}