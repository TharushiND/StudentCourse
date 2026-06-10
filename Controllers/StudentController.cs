using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.DTOs;
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
                var result = _context.Students.Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Age = s.Age
                }).ToList();
                _logger.LogInformation("200 OK - Retrieved all students successfully");
                var response =
                new CommonResponse<List<StudentDto>>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Students retrieved successfully",
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
               "Error occurred while retrieving students");
                return StatusCode(
                    500,
                    new CommonResponse<object>
                    {
                        Success = false,
                        StatusCode = 500,
                        Message = "Internal server error",
                        Data = null
                    });
            }
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] CreateStudentDto dto)
        {
            try
            {
                _logger.LogInformation(
                "Adding student {Name}",
                dto.Name);

                var student = new Student
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Age = dto.Age
                };

                _context.Students.Add(student);
                _context.SaveChanges();

                _logger.LogInformation(
                "Student created with ID {Id}",
                student.Id);

                var result = new StudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age
                };
                var response =
                new CommonResponse<StudentDto>
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "Students created successfully",
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating students");
                return StatusCode(
                    500,
                    new CommonResponse<object>
                    {
                        Success = false,
                        StatusCode = 500,
                        Message = "Internal server error",
                        Data = null
                    });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromRoute]int id,[FromBody] UpdateStudentDto dto)
        {
            try
            {
                _logger.LogInformation("Updating a student");
                var student = _context.Students.Find(id);

                if (student == null)
                {
                    _logger.LogWarning(
                    "404 Not Found-Student with ID {Id} not found", id);
                    return NotFound(
                                new CommonResponse<object>
                                {
                                    Success = false,
                                    StatusCode = 404,
                                    Message = "Student not found",
                                    Data = null
                                });
                }

                student.Name = dto.Name;
                student.Email = dto.Email;
                student.Age = dto.Age;

                _context.SaveChanges();
                _logger.LogInformation("Updated a student");

                var result = new StudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age
                };
                var response =
                    new CommonResponse<StudentDto>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Student updated successfully",
                        Data = result
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the student");
                return StatusCode(
                    500,
                    new CommonResponse<object>
                    {
                        Success = false,
                        StatusCode = 500,
                        Message = "Internal server error",
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("Deleting a student");
                var student = _context.Students.Find(id);

                if (student == null)
                {
                    _logger.LogWarning(
                    "Student with ID {Id} not found", id);
                    return NotFound(
                                new CommonResponse<object>
                                {
                                    Success = false,
                                    StatusCode = 404,
                                    Message = "Student not found",
                                    Data = null
                                });
                }

                _context.Students.Remove(student);

                _context.SaveChanges();
                _logger.LogInformation("Student with Id {Id} was deleted", student.Id);
                return Ok(
                    new CommonResponse<string>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Student deleted",
                        Data = "Student Deleted successfully"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the student");
                return StatusCode(
                    500,
                    new CommonResponse<object>
                    {
                        Success = false,
                        StatusCode = 500,
                        Message = "Internal server error",
                        Data = null
                    });
            }
        }

        [HttpPost("enroll")]
        public IActionResult EnrollStudent([FromBody] EnrollStudentDto dto)
        {
            try
            {
                _logger.LogInformation("Enrolling a student with ID {Id}", dto.StudentId);
                var student = _context.Students
                    .Include(s => s.StudentCourses)
                    .FirstOrDefault(s => s.Id == dto.StudentId);

                var course = _context.Courses
                    .FirstOrDefault(c => c.Id == dto.CourseId);

                if (student == null || course == null)
                {
                    _logger.LogWarning(
                    "Student or Course not found. StudentId: {StudentId}, CourseId: {CourseId}", dto.StudentId, dto.CourseId);

                    return NotFound(
                                new CommonResponse<object>
                                {
                                    Success = false,
                                    StatusCode = 404,
                                    Message = "not found",
                                    Data = null
                                });
                }
                var studentCourse = new StudentCourseMapping
                {
                    StudentId = dto.StudentId,
                    CourseId = dto.CourseId
                };

                _context.StudentCourses.Add(studentCourse);
                _context.SaveChanges();
                _logger.LogInformation("Student enrolled with ID {Id}", dto.StudentId);
                var response =
                    new CommonResponse<string>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Student enrolled successfully",
                        Data = "Enrollment successful"
                    };

                return Ok(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while enrolling the student");
                return StatusCode(
                    500,
                    new CommonResponse<object>
                    {
                        Success = false,
                        StatusCode = 500,
                        Message = "Internal server error",
                        Data = null
                    });
            }

        }

        [HttpGet("StudentWithCourses")]
        public IActionResult GetStudentWithCourse(
            
            )
        {
            try
            {
                _logger.LogInformation("Getting all students with courses");

                var result = _context.Students
                    .Include(s => s.StudentCourses)
                        .ThenInclude(sc => sc.Course)
                    .Select(s => new StudentWithCoursesDto
                    {
                        StudentName = s.Name,
                        Courses = s.StudentCourses.Select(sc => sc.Course.CourseName).ToList()
                    })
                    .ToList();

                var response =
                    new CommonResponse<List<StudentWithCoursesDto>>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Students with their courses retrieved successfully",
                        Data = result
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving students with courses");
                return StatusCode(
                    500,
                    new CommonResponse<object>
                    {
                        Success = false,
                        StatusCode = 500,
                        Message = "Internal server error",
                        Data = null
                    });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation(
                    "Getting student with ID {Id}",
                    id);

                var student = _context.Students
                    .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                    .FirstOrDefault(s => s.Id == id);

                if (student == null)
                {
                    _logger.LogWarning(
                        "Student with ID {Id} not found",
                        id);

                    return NotFound(
                                new CommonResponse<object>
                                {
                                    Success = false,
                                    StatusCode = 404,
                                    Message = "Student not found",
                                    Data = null
                                });
                }

                var result = new StudentDetailsDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age,

                    Courses = student.StudentCourses
                    .Select(sc => new CourseDto
                    {
                        Id = sc.Course.Id,
                        CourseName = sc.Course.CourseName,
                        Duration = sc.Course.Duration
                    })
                    .ToList()
                };

                _logger.LogInformation(
                    "Student with ID {Id} retrieved successfully",
                    id);

                var response =
                new CommonResponse<StudentDetailsDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Student details retrieved successfully",
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while retrieving student with ID {Id}",
                    id);

                return StatusCode(
                    500,
                    new CommonResponse<object>
                    {
                        Success = false,
                        StatusCode = 500,
                        Message = "Internal server error",
                        Data = null
                    });
            }
        }
    }
}