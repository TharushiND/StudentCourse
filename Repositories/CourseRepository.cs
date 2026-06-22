using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;
namespace StudentCourse.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public Course? GetCourseById(int id)
        {
            return _context.Courses
                .Include(c => c.StudentCourses)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Course> GetCoursesWithStudents()
        {
            return _context.Courses
                .Include(c => c.StudentCourses)
                .ThenInclude(sc => sc.Student)
                .ToList();
        }
        
    }
}