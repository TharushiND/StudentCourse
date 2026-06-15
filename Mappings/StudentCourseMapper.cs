using AutoMapper;
using StudentCourse.DTOs;
using StudentCourse.Models;

namespace StudentCourse.Mappings
{
    public class StudentCourseMapper : Profile
    {
        public StudentCourseMapper()
        {
            CreateMap<EnrollStudentDto,
                StudentCourseMapping>();

            CreateMap<StudentCourseMapping,
                StudentCourseDto>()
                .ForMember(
                    dest => dest.CourseId,
                    opt => opt.MapFrom(
                        src => src.Course!.Id))
                .ForMember(
                    dest => dest.CourseName,
                    opt => opt.MapFrom(
                        src => src.Course!.CourseName))
                .ForMember(
                    dest => dest.Duration,
                    opt => opt.MapFrom(
                        src => src.Course!.Duration))
                .ForMember(
                    dest => dest.Marks,
                    opt => opt.MapFrom(
                        src => src.Marks));

            CreateMap<Student,
                StudentDetailsDto>()
                .ForMember(
                    dest => dest.Courses,
                    opt => opt.MapFrom(
                        src => src.StudentCourses));

            CreateMap<StudentCourseMapping, StudentMarkDto>()
    .ForMember(
        dest => dest.StudentName,
        opt => opt.MapFrom(
            src => src.Student!.Name))
    .ForMember(
        dest => dest.CourseName,
        opt => opt.MapFrom(
            src => src.Course!.CourseName))
    .ForMember(
        dest => dest.Marks,
        opt => opt.MapFrom(
            src => src.Marks ?? 0));
        }
    }
}