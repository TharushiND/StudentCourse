using StudentCourse.Models;

namespace StudentCourse.Repositories.Interfaces
{
    public interface IStudentCourseRepository :IRepository<StudentCourseMapping>
    {
        StudentCourseMapping? GetEnrollment(int studentId,int courseId);

        List<StudentCourseMapping> GetMarksByStudent(int studentId);

        List<StudentCourseMapping> GetMarksByCourse( int courseId);

        void DeleteEnrollment(StudentCourseMapping enrollment);
    }
}