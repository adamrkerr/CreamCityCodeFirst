using Microsoft.EntityFrameworkCore;
using CreamCityCodeFirst.Context;
using CreamCityCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CreamCityCodeFirst.Repositories
{
    internal class CourseChangeHandler : ICourseChangeHandler
    {
        private readonly UniversityDbContext _context;
        private readonly ICourseRepository _repository;

        public CourseChangeHandler(UniversityDbContext context, ICourseRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<CourseDetail> CreateCourse(CourseCreationCommand courseToCreate)
        {
            var course = new Course
            {
                ClassDuration = courseToCreate.ClassDuration,
                CourseNumber = courseToCreate.CourseNumber,
                DepartmentId = courseToCreate.DepartmentId,
                Description = courseToCreate.Description,
                EndDate = courseToCreate.EndDate,
                Id = courseToCreate.Id,
                InstructorId = courseToCreate.InstructorId,
                Name = courseToCreate.Name,
                StartDate = courseToCreate.StartDate
            };

            await _context.Courses.AddAsync(course);

            await _context.SaveChangesAsync();

            return await _repository.GetCourse(courseToCreate.Id);
        }

        private static string CourseEnrollmentBatchDeleteScript = $@"
            UPDATE {nameof(UniversityDbContext.CourseEnrollments)}
            SET {nameof(CourseEnrollment.IsDeleted)} = 1,
                {nameof(CourseEnrollment.LastChangedByUser)} = @p0,
                {nameof(CourseEnrollment.LastChangedTimestamp)} = @p1
            WHERE {nameof(CourseEnrollment.CourseId)} = @p2";

        public async Task DeleteCourse(Guid courseId)
        {
            var existingCourse = await _context.Courses.FindAsync(courseId);

            existingCourse.IsDeleted = true;

            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlCommandAsync(CourseEnrollmentBatchDeleteScript,
                new SqlParameter("@p0", "test"),
                new SqlParameter("@p1", DateTimeOffset.UtcNow),
                new SqlParameter("@p2", courseId));
        }

    }
}
