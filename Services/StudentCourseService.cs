using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;
using StudentCourse.Services.Interfaces;
using AutoMapper;
using StudentCourse.Repositories;

namespace StudentCourse.Services
{
    public class StudentCourseService
        : IStudentCourseService
    {
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly StudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public StudentCourseService(
            IStudentCourseRepository studentCourseRepository, StudentRepository studentRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _studentCourseRepository =studentCourseRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        

        public bool AssignMarks(AssignMarksDto dto)
        {
            var enrollment =
                _studentCourseRepository.GetEnrollment(dto.StudentId,dto.CourseId);

            if (enrollment == null)
                return false;

            enrollment.Marks = dto.Marks;

            _studentCourseRepository.SaveChanges();

            return true;
        }

        public List<StudentMarkDto>? GetMarksByStudent(int studentId)
        {
            var student =
        _studentRepository.GetStudentById(studentId);

            if (student == null)
                return null;

            var records =
                _studentCourseRepository.GetMarksByStudent(studentId);

            return _mapper.Map<List<StudentMarkDto>>(records);
        }

        public List<StudentMarkDto>? GetMarksByCourse(int courseId)
        {
            var course =
       _courseRepository.GetCourseById(courseId);

            if (course == null)
                return null;

            var records =
                _studentCourseRepository.GetMarksByCourse(courseId);

            return _mapper.Map<List<StudentMarkDto>>(records);
        }

        public StudentMarkDto? UpdateMarks(UpdateMarksDto dto)
        {
            var updated =_studentCourseRepository.GetEnrollment(dto.StudentId,dto.CourseId);

            if (updated == null)
                return null;

            updated.Marks = dto.Marks;

            _studentCourseRepository.SaveChanges();

            return _mapper.Map<StudentMarkDto>(updated);
        }

        public StudentMarkDto? DeleteMarks(DeleteMarksDto dto)
        {
            var enrollment =_studentCourseRepository.GetEnrollment(dto.StudentId,dto.CourseId);

            if (enrollment == null)
                return null;

            enrollment.Marks = null;

            _studentCourseRepository.SaveChanges();

            return _mapper.Map<StudentMarkDto>(enrollment);
        }
    }
}