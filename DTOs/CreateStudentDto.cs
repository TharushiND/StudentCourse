using System.ComponentModel.DataAnnotations;

namespace StudentCourse.DTOs
{
    public class CreateStudentDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }
    }
}