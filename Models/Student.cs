using System.ComponentModel.DataAnnotations;

namespace StudentCourse.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }
        public ICollection<Course> Courses { get; set; }
         = new List<Course>();
    }
}