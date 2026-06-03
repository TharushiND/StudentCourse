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
            return Ok(_context.Students.ToList());
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
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

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
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

        [HttpPost("{studentId}/courses/{courseId}")]
        public IActionResult EnrollStudent(int studentId, int courseId)
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

        [HttpGet("all")]
        public IActionResult GetStudentWithCourse()
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
    }
}