using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreamCityCodeFirst.Models;

namespace CreamCityCodeFirst.Repositories
{
    public interface ICourseRepository
    {
        Task<CourseDetail> GetCourse(Guid courseId);
        Task<IEnumerable<CourseSummary>> GetCourses();
    }
}