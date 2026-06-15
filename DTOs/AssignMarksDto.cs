using StudentCourse.DTOs;

namespace StudentCourse.DTOs

{
    public class AssignMarksDto
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public int Marks {  get; set; }

    }
}
