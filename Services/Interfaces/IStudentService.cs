using StudentCourse.DTOs;

namespace StudentCourse.Services.Interfaces
{
    public interface IStudentService
    {
        List<StudentDto> GetAllStudents();

        StudentDetailsDto? GetStudentById(int id);

        StudentDto AddStudent(CreateStudentDto dto);

        StudentDto? UpdateStudent(int id, UpdateStudentDto dto);

        bool DeleteStudent(int id);

        bool EnrollStudent(EnrollStudentDto dto);

        List<StudentWithCoursesDto> GetStudentsWithCourses();
    }
}