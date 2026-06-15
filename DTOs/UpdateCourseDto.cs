using System.ComponentModel.DataAnnotations;

namespace StudentCourse.DTOs
{
    public class UpdateCourseDto
    {
        public string CourseName { get; set; }

        public string Duration { get; set; }
    }
}