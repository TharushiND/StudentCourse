using Microsoft.AspNetCore.Mvc;
using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Services.Interfaces;
using StudentCourse.Constants;

namespace StudentCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        
        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;

        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            try
            {
                _logger.LogInformation("Getting all courses");
                var courses = _courseService.GetAllCourses();
                _logger.LogInformation("200 OK - Retrieved all courses successfully");

                return Ok(
                    new CommonResponse<List<CourseDto>>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = SuccessMessages.CoursesRetrieved,
                        Data = courses
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving courses");
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
        public IActionResult AddCourse([FromBody] CreateCourseDto dto)
        {
            try
            {
                _logger.LogInformation(
                "Adding course {CourseName}",
                dto.CourseName);

                var course =_courseService.AddCourse(dto);

                _logger.LogInformation(
                "course created with ID {Id}",
                course.Id);

                return StatusCode(
                    201,
                    new CommonResponse<CourseDto>
                    {
                        Success = true,
                        StatusCode = 201,
                        Message = SuccessMessages.CourseCreated,
                        Data = course
                    });

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating course.");
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
        public IActionResult UpdateCourse([FromRoute]int id,[FromBody] UpdateCourseDto dto)
        {
            try
            {
                _logger.LogInformation("Updating a course");
                var course = _courseService.UpdateCourse(id, dto);

                if (course == null)
                {
                    return NotFound(
                        new CommonResponse<object>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ErrorMessages.CourseNotFound,
                        Data = null
                    });
                }
                    return Ok(
                            new CommonResponse<CourseDto>
                            {
                                Success = true,
                                StatusCode = 200,
                                Message = SuccessMessages.CourseUpdated,
                                Data = course
                            });
                }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the course");
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
        public IActionResult DeleteCourse([FromRoute]int id)
        {
            try
            {
                var deleted = _courseService.DeleteCourse(id);

                if (!deleted)
                {
                    return NotFound(
                        new CommonResponse<object>
                        {
                            Success = false,
                            StatusCode = 404,
                            Message = ErrorMessages.CourseNotFound,
                            Data = null
                        });
                }

                _logger.LogInformation("Course was deleted");
                return Ok(
                    new CommonResponse<string>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = SuccessMessages.CourseDeleted,
                        Data = "Course Deleted successfully"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the course");
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



        [HttpGet("CourseWithStudents")]
        public IActionResult GetCourseWithStudent()
        {
            try
            {
                _logger.LogInformation("Getting all courses with students");

                var result = _courseService.GetCoursesWithStudents();

                var response =
                    new CommonResponse<List<CourseWithStudentsDto>>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = SuccessMessages.CoursesRetrievedwithStudents,
                        Data = result
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving courses with students");
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
        public IActionResult GetCourseById([FromRoute]int id)
        {
            try
            {
                _logger.LogInformation(
                    "Getting course with ID {Id}",
                    id);

                var result = _courseService.GetCourseById(id);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Course with ID {Id} not found",
                        id);

                    return NotFound(
                                new CommonResponse<object>
                                {
                                    Success = false,
                                    StatusCode = 404,
                                    Message = ErrorMessages.CourseNotFound,
                                    Data = null
                                });
                }

                _logger.LogInformation(
                    "Course with ID {Id} retrieved successfully",
                    id);

                var response =
                new CommonResponse<CourseDetailsDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = SuccessMessages.CourseRetrievedById,
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while retrieving course with ID {Id}",
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
