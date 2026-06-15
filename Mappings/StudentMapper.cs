using AutoMapper;
using StudentCourse.DTOs;
using StudentCourse.Models;

namespace StudentCourse.Mappings
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, StudentDto>();

            CreateMap<CreateStudentDto, Student>();

            CreateMap<UpdateStudentDto, Student>();

            CreateMap<Student, StudentDetailsDto>()
                .ForMember(
                    dest => dest.Courses,
                    opt => opt.MapFrom(src => src.StudentCourses));

            CreateMap<Student, StudentWithCoursesDto>()
        .ForMember(
            dest => dest.StudentName,
            opt => opt.MapFrom(src => src.Name))
        .ForMember(
            dest => dest.Courses,
            opt => opt.MapFrom(
                src => src.StudentCourses
                    .Select(sc => sc.Course!.CourseName)));
        }
    }
}