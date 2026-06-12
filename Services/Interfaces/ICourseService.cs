using StudentCourse.DTOs;

namespace StudentCourse.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseDto> GetAllCourses();

        CourseDto AddCourse(CreateCourseDto dto);

        CourseDto? UpdateCourse(int id, UpdateCourseDto dto);

        bool DeleteCourse(int id);

        CourseDetailsDto? GetCourseById(int id);

        List<CourseWithStudentsDto> GetCoursesWithStudents();
    }
}