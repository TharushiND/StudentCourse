using Microsoft.AspNetCore.Mvc;
using StudentCourse.Constants;
using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Services.Interfaces;

namespace StudentCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }



        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                _logger.LogInformation("Getting all students");
                var students = _studentService.GetAllStudents();
                _logger.LogInformation("200 OK - Retrieved all students successfully");
                
                var response =
                new CommonResponse<List<StudentDto>>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = SuccessMessages.StudentsRetrieved,
                    Data = students
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
                        Message = ErrorMessages.InternalServerError,
                        Data = null
                    });
            }
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] CreateStudentDto dto)
        {
            try
            {
                _logger.LogInformation("Adding student {Name}",dto.Name);
                var student = _studentService.AddStudent(dto);
                _logger.LogInformation("Student created with ID {Id}",student.Id);

                return StatusCode(201,
                    new CommonResponse<StudentDto>
                    {
                        Success = true,
                        StatusCode = 201,
                        Message = SuccessMessages.StudentCreated,
                        Data = student
                    });
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
                        Message = ErrorMessages.InternalServerError,
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
                var student = _studentService.UpdateStudent(id, dto);

                if (student == null)
                {
                    _logger.LogWarning(
                    "404 Not Found-Student with ID {Id} not found", id);
                    return NotFound(
                                new CommonResponse<object>
                                {
                                    Success = false,
                                    StatusCode = 404,
                                    Message = ErrorMessages.StudentNotFound,
                                    Data = null
                                });
                }

                return Ok(
                    new CommonResponse<StudentDto>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = SuccessMessages.StudentUpdated,
                        Data = student
                    });
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
                        Message = ErrorMessages.InternalServerError,
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
                var deleted = _studentService.DeleteStudent(id);

                if (!deleted)
                {
                    _logger.LogWarning(
                    "Student with ID {Id} not found", id);
                    return NotFound(
                                new CommonResponse<object>
                                {
                                    Success = false,
                                    StatusCode = 404,
                                    Message = ErrorMessages.StudentNotFound,
                                    Data = "Student not found"
                                });
                }

                _logger.LogInformation("Student was deleted");
                return Ok(
                    new CommonResponse<string>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = SuccessMessages.StudentDeleted,
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
                        Message = ErrorMessages.InternalServerError,
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
                var enrolled = _studentService.EnrollStudent(dto);

                
                if (!enrolled)
                {
                    _logger.LogWarning(
                    "Student or Course not found. StudentId: {StudentId}, CourseId: {CourseId}", dto.StudentId, dto.CourseId);

                    return NotFound(
                                new CommonResponse<object>
                                {
                                    Success = false,
                                    StatusCode = 404,
                                    Message = ErrorMessages.StudentNotFound,
                                    Data = null
                                });
                }

                return Ok(
                    new CommonResponse<string>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = SuccessMessages.StudentEnrolled,
                        Data = "Enrollment successful"
                    });
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
                        Message = ErrorMessages.InternalServerError,
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

                var result = _studentService.GetStudentsWithCourses();

                var response =
                    new CommonResponse<List<StudentWithCoursesDto>>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = SuccessMessages.StudentsRetrievedwithCourses,
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
                        Message = ErrorMessages.InternalServerError,
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

                var student = _studentService.GetStudentById(id);

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
                                    Message = ErrorMessages.StudentNotFound,
                                    Data = null
                                });
                }

                _logger.LogInformation(
                    "Student with ID {Id} retrieved successfully",
                    id);

                return Ok(
                    new CommonResponse<StudentDetailsDto>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = SuccessMessages.StudentRetrievedById,
                        Data = student
                    });
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
                        Message = ErrorMessages.InternalServerError,
                        Data = null
                    });
            }
        }
    }
}