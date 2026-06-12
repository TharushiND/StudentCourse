using StudentCourse.DTOs;
using StudentCourse.Models;
using StudentCourse.Repositories.Interfaces;
using StudentCourse.Services.Interfaces;

namespace StudentCourse.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<StudentDto> GetAllStudents()
        {
            var students = _studentRepository.GetAllStudents();

            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Age = s.Age
            }).ToList();
        }

        public StudentDetailsDto? GetStudentById(int id)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                return null;

            return new StudentDetailsDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Courses = student.StudentCourses
                    .Select(sc => new CourseDto
                    {
                        Id = sc.Course!.Id,
                        CourseName = sc.Course.CourseName,
                        Duration = sc.Course.Duration
                    }).ToList()
            };
        }

        public StudentDto AddStudent(CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age
            };

            _studentRepository.AddStudent(student);
            _studentRepository.SaveChanges();

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age
            };
        }

        public StudentDto? UpdateStudent(int id, UpdateStudentDto dto)
        {
            var student = _studentRepository.GetStudentById(id);

            if (student == null)
                return null;

            student.Name = dto.Name;
            student.Email = dto.Email;
            student.Age = dto.Age;

            _studentRepository.UpdateStudent(student);
            _studentRepository.SaveChanges();

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age
            };
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
            var studentCourse = new StudentCourseMapping
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId
            };

            _studentRepository.EnrollStudent(studentCourse);
            _studentRepository.SaveChanges();

            return true;
        }

        public List<StudentWithCoursesDto> GetStudentsWithCourses()
        {
            var students = _studentRepository.GetStudentsWithCourses();

            return students.Select(s => new StudentWithCoursesDto
            {
                StudentName = s.Name,
                Courses = s.StudentCourses
                    .Select(sc => sc.Course!.CourseName)
                    .ToList()
            }).ToList();
        }
    }
}