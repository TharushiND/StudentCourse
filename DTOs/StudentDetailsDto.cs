namespace StudentCourse.DTOs
{
    public class StudentDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public List<CourseDto> Courses { get; set; }
            = new();
    }
}