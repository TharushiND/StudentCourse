namespace StudentCourse.DTOs
{
    public class CourseWithStudentsDto
    {
        public string CourseName { get; set; }

        public List<string> Students { get; set; }= new ();
    }
}