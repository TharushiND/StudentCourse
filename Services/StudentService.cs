using AutoMapper;
using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;
using StudentCourse.Services.Interfaces;

namespace StudentCourse.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public List<StudentDto> GetAllStudents()
        {
            var students = _studentRepository.GetAllStudents();

            return _mapper.Map<List<StudentDto>>(students);
        }
        public StudentDetailsDto? GetStudentById(int id)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                return null;

            return _mapper.Map<StudentDetailsDto>(student);
        }

        public StudentDto AddStudent(CreateStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);

            _studentRepository.AddStudent(student);
            _studentRepository.SaveChanges();

            return _mapper.Map<StudentDto>(student);
        }

        public StudentDto? UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                return null;

            _mapper.Map(dto, student);

            _studentRepository.UpdateStudent(student);
            _studentRepository.SaveChanges();

            return _mapper.Map<StudentDto>(student);
        }

        public bool DeleteStudent(int id)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                return false;

            _studentRepository.DeleteStudent(student);
            _studentRepository.SaveChanges();

            return true;
        }

        public bool EnrollStudent(EnrollStudentDto dto)
        {
            var studentCourse = _mapper.Map<StudentCourseMapping>(dto);

            _studentRepository.EnrollStudent(studentCourse);
            _studentRepository.SaveChanges();

            return true;
        }

        public List<StudentWithCoursesDto> GetStudentsWithCourses()
        {
            var students = _studentRepository.GetStudentsWithCourses();

            return _mapper.Map<List<StudentWithCoursesDto>>(students);
        }
    }
}