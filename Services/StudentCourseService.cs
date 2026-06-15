using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;
using StudentCourse.Services.Interfaces;
using AutoMapper;

namespace StudentCourse.Services
{
    public class StudentCourseService
        : IStudentCourseService
    {
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly IMapper _mapper;

        public StudentCourseService(
            IStudentCourseRepository studentCourseRepository, IMapper mapper)
        {
            _studentCourseRepository =studentCourseRepository;
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

        public List<StudentMarkDto>GetMarksByStudent(int studentId)
        {
            var records = _studentCourseRepository.GetMarksByStudent(studentId);

            return _mapper.Map<List<StudentMarkDto>>(records);
        }

        public List<StudentMarkDto>GetMarksByCourse(int courseId)
        {
            var records =_studentCourseRepository.GetMarksByCourse(courseId);

            return _mapper.Map<List<StudentMarkDto>>(records);
        }

        public bool UpdateMarks(UpdateMarksDto dto)
        {
            var enrollment =_studentCourseRepository.GetEnrollment(dto.StudentId,dto.CourseId);

            if (enrollment == null)
                return false;

            enrollment.Marks = dto.Marks;

            _studentCourseRepository.SaveChanges();

            return true;
        }

        public bool DeleteMarks(DeleteMarksDto dto)
        {
            var enrollment =_studentCourseRepository.GetEnrollment(dto.StudentId,dto.CourseId);

            if (enrollment == null)
                return false;

            enrollment.Marks = null;

            _studentCourseRepository.SaveChanges();

            return true;
        }
    }
}