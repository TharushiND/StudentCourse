using System.ComponentModel.DataAnnotations;

namespace StudentCourse.DTOs
{
    public class CreateCourseDto
    {
        public string CourseName { get; set; }

        public string? Duration { get; set; }
    }
}