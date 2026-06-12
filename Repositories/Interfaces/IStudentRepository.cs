using StudentCourse.Models;

namespace StudentCourse.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudents();

        Student? GetStudentById(int id);

        void AddStudent(Student student);

        void UpdateStudent(Student student);

        void DeleteStudent(Student student);

        void EnrollStudent(StudentCourseMapping studentCourse);

        List<Student> GetStudentsWithCourses();
        void SaveChanges();
    }
}