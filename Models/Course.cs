using System.ComponentModel.DataAnnotations;

namespace StudentCourse.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string CourseName { get; set; }

        
        [StringLength(30)]
        public string? Duration { get; set; }

        //public DateTime StartDate { get; set; }

        public ICollection<StudentCourseMapping> StudentCourses { get; set; }
          = new List<StudentCourseMapping>();
    }
}