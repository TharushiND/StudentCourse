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

        StudentMarkDto? UpdateMarks(UpdateMarksDto dto);

        StudentMarkDto? DeleteMarks(DeleteMarksDto dto);
    }
}