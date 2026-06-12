using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;

namespace StudentCourse.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Student? GetStudentById(int id)
        {
            return _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.Id == id);
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
        }

        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
        }

        public void DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
        }

        public void EnrollStudent(StudentCourseMapping studentCourse)
        {
            _context.StudentCourses.Add(studentCourse);
        }

        public List<Student> GetStudentsWithCourses()
        {
            return _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}