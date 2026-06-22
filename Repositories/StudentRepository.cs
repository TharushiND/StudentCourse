using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;

namespace StudentCourse.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public Student? GetStudentById(int id)
        {
            return _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.Id == id);
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
    }
}