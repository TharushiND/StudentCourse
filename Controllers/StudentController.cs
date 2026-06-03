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

        public StudentController(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _context.Students.ToList();
                return Ok(students);
            }
            catch (Exception ex) {
                return StatusCode(500, $"an error occurred! {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();

                return Ok(student);
            }
            catch (Exception ex) {
                return StatusCode(500,
                $"an error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
        {
            try
            {
                var student = _context.Students.Find(id);

                if (student == null)
                {
                    return NotFound();
                }

                student.Name = updatedStudent.Name;
                student.Email = updatedStudent.Email;
                student.Age = updatedStudent.Age;

                _context.SaveChanges();

                return Ok(student);
            }
            catch (Exception ex) {
                return StatusCode(500,
                $"an error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var student = _context.Students.Find(id);

                if (student == null)
                {
                    return NotFound();
                }

                _context.Students.Remove(student);

                _context.SaveChanges();

                return Ok("Student Deleted Successfully");
            }
            catch (Exception ex) {
                return StatusCode(500,
                $"an error occurred: {ex.Message}");
            }
        }

        [HttpPost("{studentId}/courses/{courseId}")]
        public IActionResult EnrollStudent(int studentId, int courseId)
        {
            try
            {
                var student = _context.Students
                    .Include(s => s.Courses)
                    .FirstOrDefault(s => s.Id == studentId);

                var course = _context.Courses
                    .FirstOrDefault(c => c.Id == courseId);

                if (student == null || course == null)
                    return NotFound();

                student.Courses.Add(course);

                _context.SaveChanges();

                return Ok("Enrollment successful");
            }

            catch (Exception ex) {
                    return StatusCode(500,
                    $"an error occurred: {ex.Message}");
                }
            
            }

        [HttpGet("all")]
        public IActionResult GetStudentWithCourse()
        {
            try
            {
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
                return StatusCode(500,
                $"an error occurred: {ex.Message}");
            }
        }
    }
}