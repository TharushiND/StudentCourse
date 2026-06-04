using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;
using Serilog;

namespace StudentCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseController> _logger;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            try
            {
                _logger.LogInformation("Getting all courses");
                return Ok(_context.Courses.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving courses");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            try
            {
                _logger.LogInformation(
                "Adding course {CourseName}",
                course.CourseName);

                _context.Courses.Add(course);
                _context.SaveChanges();

                _logger.LogInformation(
                "course created with ID {Id}",
                course.Id);

                return Ok(course);
            }
            catch (Exception ex) {
                _logger.LogError("Error occurred while creating course.");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, Course updatedCourse)
        {
            try
            {
                _logger.LogInformation("Updating a course");
                var course = _context.Courses.Find(id);

                if (course == null)
                {
                    return NotFound();
                }

                course.CourseName = updatedCourse.CourseName;
                course.Duration = updatedCourse.Duration;

                _context.SaveChanges();
                _logger.LogInformation("Updated a course");

                return Ok(course);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while updating the course");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            try
            {
                var course = _context.Courses.Find(id);

                if (course == null)
                {
                    return NotFound();
                }

                _context.Courses.Remove(course);

                _context.SaveChanges();

                _logger.LogInformation("Course with Id {Id} was deleted", course.Id);
                return Ok("Course Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the course");
                return StatusCode(500);
            }
        }
            
           

        [HttpGet("all")]
        public IActionResult GetCourseWithStudent()
        {
            try
            {
                _logger.LogInformation("Getting all courses with students");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while retrieving courses with students");
                return StatusCode(500);
            }
        }
    }
}
