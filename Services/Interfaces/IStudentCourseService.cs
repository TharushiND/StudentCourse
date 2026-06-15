using StudentCourse.DTOs;

namespace StudentCourse.Services.Interfaces
{
    public interface IStudentCourseService
    {

        bool AssignMarks(
            AssignMarksDto dto);

        List<StudentMarkDto> GetMarksByStudent(
            int studentId);

        List<StudentMarkDto> GetMarksByCourse(
            int courseId);

        bool UpdateMarks(UpdateMarksDto dto);

        bool DeleteMarks(DeleteMarksDto dto);
    }
}