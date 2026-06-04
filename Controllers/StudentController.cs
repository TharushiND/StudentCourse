using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.Models;

namespace StudentCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentController> _logger;

        public StudentController(AppDbContext context, ILogger<StudentController> logger)
        {
            _context = context;
            _logger = logger;
        }



        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                _logger.LogInformation("Getting all students");
                var students = _context.Students.ToList();
                return Ok(students);
            }
            catch (Exception ex) {
             _logger.LogError(ex,
            "Error occurred while retrieving students");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            try
            {
                _logger.LogInformation(
                "Adding student {Name}",
                student.Name);

                _context.Students.Add(student);
                _context.SaveChanges();

                _logger.LogInformation(
                "Student created with ID {Id}",
                student.Id);

                return Ok(student);
            }
            catch (Exception ex) {
                _logger.LogError("Error occurred while creating students");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
        {
            try
            {
                _logger.LogInformation("Updating a student");
                var student = _context.Students.Find(id);

                if (student == null)
                {
                    return NotFound();
                }

                student.Name = updatedStudent.Name;
                student.Email = updatedStudent.Email;
                student.Age = updatedStudent.Age;

                _context.SaveChanges();
                _logger.LogInformation("Updated a student");
                return Ok(student);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while updating the student");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _logger.LogInformation("Deleting a student");
                var student = _context.Students.Find(id);

                if (student == null)
                {
                    return NotFound();
                }

                _context.Students.Remove(student);

                _context.SaveChanges();
                _logger.LogInformation("Student with Id {Id} was deleted",student.Id);
                return Ok("Student Deleted Successfully");
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while deleting the student");
                return StatusCode(500);
            }
        }

        [HttpPost("{studentId}/courses/{courseId}")]
        public IActionResult EnrollStudent(int studentId, int courseId)
        {
            try
            {
                _logger.LogInformation("Enrolling a student with ID {Id}",studentId );
                var student = _context.Students
                    .Include(s => s.Courses)
                    .FirstOrDefault(s => s.Id == studentId);

                var course = _context.Courses
                    .FirstOrDefault(c => c.Id == courseId);

                if (student == null || course == null)
                    return NotFound();

                student.Courses.Add(course);

                _context.SaveChanges();
                _logger.LogInformation("Student enrolled with ID {Id}", studentId);
                return Ok("Enrollment successful");
            }

            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while enrolling the student");
                return StatusCode(500);
                }
            
            }

        [HttpGet("all")]
        public IActionResult GetStudentWithCourse()
        {
            try
            {
                _logger.LogInformation("Getting all students with courses");
                var result = _context.Students
                    .Include(s => s.Courses)
                    .Select(s => new
                    {
                        StudentName = s.Name,
                        CourseName = s.Courses.Select(c => c.CourseName).ToList()
                    })
                    .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while retrieving students with courses");
                return StatusCode(500);
            }
        }
    }
}