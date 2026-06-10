namespace StudentCourse.DTOs
{
    public class CourseDetailsDto
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string? Duration { get; set; }

        public List<StudentDto> Students { get; set; }
            = new();
    }
}
