using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;

namespace StudentCourse.Repositories
{
    public class StudentCourseRepository
        : IStudentCourseRepository
    {
        private readonly AppDbContext _context;

        public StudentCourseRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public void EnrollStudent(
            StudentCourseMapping studentCourse)
        {
            _context.StudentCourses.Add(studentCourse);
        }

        public StudentCourseMapping? GetEnrollment(
            int studentId,
            int courseId)
        {
            return _context.StudentCourses
                .Include(x => x.Student)
                .Include(x => x.Course)
                .FirstOrDefault(x =>
                    x.StudentId == studentId &&
                    x.CourseId == courseId);
        }

        public void DeleteEnrollment(
    StudentCourseMapping enrollment)
        {
            _context.StudentCourses.Remove(enrollment);
        }
        public List<StudentCourseMapping> GetMarksByStudent(
            int studentId)
        {
            return _context.StudentCourses
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Where(x => x.StudentId == studentId)
                .ToList();
        }

        public List<StudentCourseMapping> GetMarksByCourse(
            int courseId)
        {
            return _context.StudentCourses
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Where(x => x.CourseId == courseId)
                .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}