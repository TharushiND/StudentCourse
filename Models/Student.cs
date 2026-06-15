

namespace StudentCourse.Models
{
    public class Student
    {
        public int Id { get; set; }

        
        public required string Name { get; set; }

        public required string Email { get; set; }

        public int? Age { get; set; }
        public ICollection<StudentCourseMapping> StudentCourses { get; set; }
         = new List<StudentCourseMapping>();
    }
}