using Microsoft.AspNetCore.Mvc;
using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Services.Interfaces;

namespace StudentCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarksController : ControllerBase
    {
        private readonly IStudentCourseService _studentCourseService;
        private readonly ILogger<MarksController> _logger;

        public MarksController(
            IStudentCourseService studentCourseService,
            ILogger<MarksController> logger)
        {
            _studentCourseService = studentCourseService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AssignMarks(
            [FromBody] AssignMarksDto dto)
        {
            try
            {
                _logger.LogInformation(
                    "Assigning marks for StudentId {StudentId} and CourseId {CourseId}",
                    dto.StudentId,
                    dto.CourseId);

                var success =
                    _studentCourseService.AssignMarks(dto);

                if (!success)
                {
                    return NotFound(
                        new CommonResponse<object>
                        {
                            Success = false,
                            StatusCode = 404,
                            Message = "Enrollment not found",
                            Data = null
                        });
                }

                return Ok(
                    new CommonResponse<object>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Marks assigned successfully",
                        Data = dto
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while assigning marks");

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

        [HttpGet("student/{studentId}")]
        public IActionResult GetMarksByStudent(
            [FromRoute] int studentId)
        {
            try
            {
                var result =
                    _studentCourseService
                    .GetMarksByStudent(studentId);

                return Ok(
                    new CommonResponse<List<StudentMarkDto>>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Student marks retrieved successfully",
                        Data = result
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error retrieving marks for student");

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

        [HttpGet("course/{courseId}")]
        public IActionResult GetMarksByCourse(
            [FromRoute] int courseId)
        {
            try
            {
                var result =
                    _studentCourseService
                    .GetMarksByCourse(courseId);

                return Ok(
                    new CommonResponse<List<StudentMarkDto>>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Course marks retrieved successfully",
                        Data = result
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error retrieving course marks");

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

        [HttpPut]
        public IActionResult UpdateMarks(
    [FromBody] UpdateMarksDto dto)
        {
            try
            {
                var success =
                    _studentCourseService
                    .UpdateMarks(dto);

                if (!success)
                {
                    return NotFound(
                        new CommonResponse<object>
                        {
                            Success = false,
                            StatusCode = 404,
                            Message = "Enrollment not found",
                            Data = null
                        });
                }

                return Ok(
                    new CommonResponse<UpdateMarksDto>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Marks updated successfully",
                        Data = dto
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating marks");

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

        [HttpDelete]
        public IActionResult DeleteMarks(
    [FromBody] DeleteMarksDto dto)
        {
            try
            {
                var success =
                    _studentCourseService
                    .DeleteMarks(dto);

                if (!success)
                {
                    return NotFound(
                        new CommonResponse<object>
                        {
                            Success = false,
                            StatusCode = 404,
                            Message = "Enrollment not found",
                            Data = null
                        });
                }

                return Ok(
                    new CommonResponse<object>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Marks deleted successfully",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting marks");

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