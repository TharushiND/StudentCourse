using StudentCourse.Models;

namespace StudentCourse.Repositories.Interfaces
{
    public interface ICourseRepository :IRepository<Course>
    {

        Course? GetCourseById(int id);

        List<Course> GetCoursesWithStudents();

    }
}