using AutoMapper;
using StudentCourse.DTOs;
using StudentCourse.Models;

namespace StudentCourse.Mappings
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseDto>();

            CreateMap<CreateCourseDto, Course>();

            CreateMap<UpdateCourseDto, Course>();

            CreateMap<Course, CourseWithStudentsDto>()
                .ForMember(
                    dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.CourseName))
                .ForMember(
                    dest => dest.Students,
                    opt => opt.MapFrom(
                        src => src.StudentCourses
                            .Select(sc => sc.Student!.Name)));

            CreateMap<Course, CourseDetailsDto>()
                .ForMember(
                    dest => dest.Students,
                    opt => opt.MapFrom(
                        src => src.StudentCourses
                            .Select(sc => sc.Student)));

            CreateMap<Student, StudentDto>();
        }
    }
}