using AutoMapper;
using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;
using StudentCourse.Services.Interfaces;

namespace StudentCourse.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public List<CourseDto> GetAllCourses()
        {
            var courses = _courseRepository.GetAllCourses();

            return _mapper.Map<List<CourseDto>>(courses);
        }

        public CourseDetailsDto? GetCourseById(int id)
        {
            var course = _courseRepository.GetCourseById(id);

            if (course == null)
                return null;

            return _mapper.Map<CourseDetailsDto>(course);
        }

        public CourseDto AddCourse(CreateCourseDto dto)
        {
            var course = _mapper.Map<Course>(dto);

            _courseRepository.AddCourse(course);
            _courseRepository.SaveChanges();

            return _mapper.Map<CourseDto>(course);
        }

        public CourseDto? UpdateCourse(
            int id,
            UpdateCourseDto dto)
        {
            var course =
                _courseRepository.GetCourseById(id);

            if (course == null)
                return null;

            _mapper.Map(dto, course);

            _courseRepository.UpdateCourse(course);
            _courseRepository.SaveChanges();

            return _mapper.Map<CourseDto>(course);
        }

        public bool DeleteCourse(int id)
        {
            var course =
                _courseRepository.GetCourseById(id);

            if (course == null)
                return false;

            _courseRepository.DeleteCourse(course);
            _courseRepository.SaveChanges();

            return true;
        }

        public List<CourseWithStudentsDto>
            GetCoursesWithStudents()
        {
            var courses =
                _courseRepository.GetCoursesWithStudents();

            return _mapper.Map<
                List<CourseWithStudentsDto>>(courses);
        }
    }
}