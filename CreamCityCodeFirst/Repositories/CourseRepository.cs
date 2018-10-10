using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CreamCityCodeFirst.Context;
using CreamCityCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace CreamCityCodeFirst.Repositories
{
    internal class CourseRepository : ICourseRepository
    {
        private readonly UniversityDbContextReadonly _context;
        public CourseRepository(UniversityDbContextReadonly context)
        {
            _context = context;
        }

        public async Task<CourseDetail> GetCourse(Guid courseId)
        {
            var course = _context.Courses
                        .Include(c => c.Department)
                        .Include(c => c.Instructor)
                        .ThenInclude(c => c.Department)
                        .Include(c => c.EnrolledStudents)
                        .ThenInclude(c => c.Student)
                        .Where(c => c.Id == courseId)
                        .ProjectTo<CourseDetail>() //Automapper
                        .SingleOrDefault();

            return course;

            //Not needed because of ProjectTo<>
            //return Mapper.Map<CourseDetail>(course);
        }

        public async Task<IEnumerable<CourseSummary>> GetCourses()
        {
            var courses = _context.Courses
                        .Include(c => c.Department)
                        .Include(c => c.Instructor)
                        .Include(c => c.EnrolledStudents);

            return Mapper.Map<List<CourseSummary>>(courses);
        }
    }
}
