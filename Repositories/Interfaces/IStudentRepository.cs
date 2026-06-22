using StudentCourse.Models;

namespace StudentCourse.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student? GetStudentById(int id);

        void EnrollStudent(StudentCourseMapping studentCourse);

        List<Student> GetStudentsWithCourses();
    }
}