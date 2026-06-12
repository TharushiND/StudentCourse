using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;

namespace StudentCourse.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course? GetCourseById(int id)
        {
            return _context.Courses
                .Include(c => c.StudentCourses)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefault(c => c.Id == id);
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
        }

        public void DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
        }

        public List<Course> GetCoursesWithStudents()
        {
            return _context.Courses
                .Include(c => c.StudentCourses)
                .ThenInclude(sc => sc.Student)
                .ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}