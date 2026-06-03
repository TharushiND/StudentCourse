using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;


namespace StudentCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            return Ok(_context.Courses.ToList());
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();

            return Ok(course);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, Course updatedCourse)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            course.CourseName = updatedCourse.CourseName;
            course.Duration = updatedCourse.Duration;

            _context.SaveChanges();

            return Ok(course);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);

            _context.SaveChanges();

            return Ok("Course Deleted Successfully");
        }

        [HttpGet("all")]
        public IActionResult GetCourseWithStudent()
        {
            var result = _context.Courses
                .Include(c => c.Students)
                .Select(c => new
                {
                    CourseName = c.CourseName,
                    StudentName = c.Students.Select(s => s.Name).ToList()
                })
                .ToList();

            return Ok(result);
        }
    }
}
