using AutoMapper;
using CreamCityCodeFirst.Context;
using CreamCityCodeFirst.Models;

namespace CreamCityCodeFirst.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDetail>();

            CreateMap<CourseEnrollment, CourseEnrollmentDetail>();

            CreateMap<Department, DepartmentDetail>();

            CreateMap<Instructor, InstructorDetail>();

            CreateMap<Student, StudentDetail>();

            CreateMap<Course, CourseSummary>()
                .ForMember(s => s.DepartmentName, o => o.MapFrom(c => c.Department.Name))
                .ForMember(s => s.ClassDurationMinutes, o => o.MapFrom(c => c.ClassDuration.Value.TotalMinutes))
                .ForMember(s => s.InstructorName, o => o.MapFrom(c => $"{c.Instructor.Title}. {c.Instructor.FirstName} {c.Instructor.LastName}"))
                .ForMember(s => s.DepartmentName, o => o.MapFrom(c => c.Department.Name))
                .ForMember(s => s.NumberStudentsEnrolled, o => o.MapFrom(c => c.EnrolledStudents.Count)); ;
        }
    }
}
