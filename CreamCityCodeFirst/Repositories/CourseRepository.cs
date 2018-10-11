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
using System.Data;
using System.Data.SqlClient;

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

        private const string CoursesJoinOnNumber = 
            "SELECT crs.* FROM " + nameof(UniversityDbContext.Courses) + " crs " +
            "INNER JOIN @courseNumbers cn ON cn.CourseNumber = crs." + nameof(Course.CourseNumber);

        public async Task<IEnumerable<CourseSummary>> GetCourses(IEnumerable<int> courseNumbers)
        {
            var cnTable = new DataTable();
            cnTable.Columns.Add("CourseNumber", typeof(int));

            foreach (var cn in courseNumbers)
            {
                cnTable.Rows.Add(cn);
            }

            var numbersParam = new SqlParameter("@courseNumbers", cnTable)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = UniversityDbContext.CourseNumberTableTypeName
            };

            var courses = _context.Courses
                        .FromSql<Course>(CoursesJoinOnNumber, numbersParam)
                        .Include(c => c.Department) //note this is a queryable and we can still do includes on it
                        .Include(c => c.Instructor)
                        //.Include(c => c.EnrolledStudents) interestingly, this cannot be loaded
                        .ToList();

            return Mapper.Map<List<CourseSummary>>(courses);
        }
    }
}
