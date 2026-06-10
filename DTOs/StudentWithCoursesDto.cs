namespace StudentCourse.DTOs
{
    public class StudentWithCoursesDto
    {
        public string StudentName { get; set; }

        public List<string> Courses { get; set; } = new ();
    }
}