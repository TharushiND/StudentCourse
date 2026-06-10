using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Data;
using StudentCourse.DTOs;
using StudentCourse.Models;

namespace StudentCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseController> _logger;

        public CourseController(AppDbContext context, ILogger<CourseController> logger)
        {
            _context = context;
            _logger = logger;

        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            try
            {
                _logger.LogInformation("Getting all courses");
                var result = _context.Courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    Duration = c.Duration,
                }).ToList();
                _logger.LogInformation("200 OK - Retrieved all courses successfully");
                var response =
                new CommonResponse<List<CourseDto>>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Courses retrieved successfully",
                    Data = result
                };

                return Ok(response);
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
                        Message = "Internal server error",
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

                var course = new Course
                {
                    CourseName = dto.CourseName,
                    Duration = dto.Duration
                };

                _context.Courses.Add(course);
                _context.SaveChanges();

                _logger.LogInformation(
                "course created with ID {Id}",
                course.Id);

                var result = new CourseDto
                {
                    Id = course.Id,
                    CourseName = dto.CourseName,
                    Duration = dto.Duration
                };
                var response =
                new CommonResponse<CourseDto>
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "Course created successfully",
                    Data = result
                };

                return Ok(response);

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
                        Message = "Internal server error",
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
                var course = _context.Courses.Find(id);

                if (course == null)
                {
                    return NotFound();
                }

                course.CourseName = dto.CourseName;
                course.Duration = dto.Duration;

                _context.SaveChanges();
                _logger.LogInformation("Updated a course");

                var result = new CourseDto
                {
                    Id = course.Id,
                    CourseName = dto.CourseName,
                    Duration = dto.Duration
                };
                var response =
                    new CommonResponse<CourseDto>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Course updated successfully",
                        Data = result
                    };

                return Ok(response);
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
                        Message = "Internal server error",
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse([FromQuery]int id)
        {
            try
            {
                var course = _context.Courses.Find(id);

                if (course == null)
                {
                    return NotFound(
                        new CommonResponse<object>
                        {
                            Success = false,
                            StatusCode = 404,
                            Message = "Course not found",
                            Data = null
                        });
                }

                _context.Courses.Remove(course);

                _context.SaveChanges();

                _logger.LogInformation("Course with Id {Id} was deleted", course.Id);
                return Ok(
                    new CommonResponse<string>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Course deleted",
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
                        Message = "Internal server error",
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

                var result = _context.Courses
                    .Include(c => c.StudentCourses)
                        .ThenInclude(sc => sc.Student)
                    .Select(c => new CourseWithStudentsDto
                    {
                        CourseName = c.CourseName,
                        Students = c.StudentCourses.Select(sc => sc.Student.Name).ToList()
                    })
                    .ToList();

                var response =
                    new CommonResponse<List<CourseWithStudentsDto>>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Courses with Students retrieved successfully",
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
                        Message = "Internal server error",
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

                var course = _context.Courses
                    .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                    .FirstOrDefault(c => c.Id == id);

                if (course == null)
                {
                    _logger.LogWarning(
                        "Course with ID {Id} not found",
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

                var result = new CourseDetailsDto
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Duration = course.Duration,

                    Students = course.StudentCourses
                    .Select(sc => new StudentDto
                    {
                        Id = sc.Student.Id,
                        Name = sc.Student.Name,
                        Email = sc.Student.Email,
                        Age = sc.Student.Age
                    })
                    .ToList()
                };

                _logger.LogInformation(
                    "Course with ID {Id} retrieved successfully",
                    id);

                var response =
                new CommonResponse<CourseDetailsDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Course details retrieved successfully",
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
                        Message = "Internal server error",
                        Data = null
                    });
            }
        }
    }
}
