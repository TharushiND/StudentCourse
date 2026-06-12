using StudentCourse.Models;

namespace StudentCourse.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();

        Course? GetCourseById(int id);

        void AddCourse(Course course);

        void UpdateCourse(Course course);

        void DeleteCourse(Course course);

        List<Course> GetCoursesWithStudents();

        void SaveChanges();
    }
}