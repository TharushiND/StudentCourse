using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;
using StudentCourse.Services.Interfaces;

namespace StudentCourse.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<CourseDto> GetAllCourses()
        {
            var courses = _courseRepository.GetAllCourses();

            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                CourseName = c.CourseName,
                Duration = c.Duration
            }).ToList();
        }

        public CourseDto AddCourse(CreateCourseDto dto)
        {
            var course = new Course
            {
                CourseName = dto.CourseName,
                Duration = dto.Duration
            };

            _courseRepository.AddCourse(course);
            _courseRepository.SaveChanges();

            return new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Duration = course.Duration
            };
        }

        public CourseDto? UpdateCourse(int id, UpdateCourseDto dto)
        {
            var course = _courseRepository.GetCourseById(id);

            if (course == null)
                return null;

            course.CourseName = dto.CourseName;
            course.Duration = dto.Duration;

            _courseRepository.UpdateCourse(course);
            _courseRepository.SaveChanges();

            return new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Duration = course.Duration
            };
        }

        public bool DeleteCourse(int id)
        {
            var course = _courseRepository.GetCourseById(id);

            if (course == null)
                return false;

            _courseRepository.DeleteCourse(course);
            _courseRepository.SaveChanges();

            return true;
        }

        public List<CourseWithStudentsDto> GetCoursesWithStudents()
        {
            var courses = _courseRepository.GetCoursesWithStudents();

            return courses.Select(c => new CourseWithStudentsDto
            {
                CourseName = c.CourseName,
                Students = c.StudentCourses
                    .Select(sc => sc.Student!.Name)
                    .ToList()
            }).ToList();
        }

        public CourseDetailsDto? GetCourseById(int id)
        {
            var course = _courseRepository.GetCourseById(id);

            if (course == null)
                return null;

            return new CourseDetailsDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Duration = course.Duration,

                Students = course.StudentCourses
                    .Select(sc => new StudentDto
                    {
                        Id = sc.Student!.Id,
                        Name = sc.Student.Name,
                        Email = sc.Student.Email,
                        Age = sc.Student.Age
                    })
                    .ToList()
            };
        }
    }
}