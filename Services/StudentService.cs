using AutoMapper;
using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Repositories;
using StudentCourse.Repositories.Interfaces;
using StudentCourse.Services.Interfaces;

namespace StudentCourse.Services
{
    public class StudentService :IStudentService
    {
        private readonly StudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(
            StudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public List<StudentDto> GetAllStudents()
        {
            var students = _studentRepository.GetAll();

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

            _studentRepository.Add(student);
            _studentRepository.SaveChanges();

            return _mapper.Map<StudentDto>(student);
        }

        public StudentDto? UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                return null;

            _mapper.Map(dto, student);

            _studentRepository.Update(student);
            _studentRepository.SaveChanges();

            return _mapper.Map<StudentDto>(student);
        }

        public StudentDto? DeleteStudent(int id)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                return null;

            var deletedStudent =_mapper.Map<StudentDto>(student);

            _studentRepository.Delete(student);
            _studentRepository.SaveChanges();

            return deletedStudent;
        }

        public EnrollStudentDto? EnrollStudent(EnrollStudentDto dto)
        {
            var studentCourse =_mapper.Map<StudentCourseMapping>(dto);

            _studentRepository.EnrollStudent(studentCourse);
            _studentRepository.SaveChanges();

            return _mapper.Map<EnrollStudentDto>(studentCourse);
        }

        public List<StudentWithCoursesDto> GetStudentsWithCourses()
        {
            var students = _studentRepository.GetStudentsWithCourses();

            return _mapper.Map<List<StudentWithCoursesDto>>(students);
        }
    }
}