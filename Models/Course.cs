

namespace StudentCourse.Models
{
    public class Course
    {
        public int Id { get; set; }

       
        public required string CourseName { get; set; }

        public required string Duration { get; set; }


        public ICollection<StudentCourseMapping> StudentCourses { get; set; }
          = new List<StudentCourseMapping>();
    }
}