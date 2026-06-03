using System.ComponentModel.DataAnnotations;

namespace StudentCourse.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Range(18, 100)]
        public int? Age { get; set; }
        public ICollection<Course> Courses { get; set; }
         = new List<Course>();
    }
}