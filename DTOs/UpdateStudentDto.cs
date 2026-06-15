using System.ComponentModel.DataAnnotations;

namespace StudentCourse.DTOs
{
    public class UpdateStudentDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }
    }
}