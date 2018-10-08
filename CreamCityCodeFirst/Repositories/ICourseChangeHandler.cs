using System;
using System.Threading.Tasks;
using CreamCityCodeFirst.Models;

namespace CreamCityCodeFirst.Repositories
{
    public interface ICourseChangeHandler
    {
        Task<CourseDetail> CreateCourse(CourseCreationCommand courseToCreate);
        Task DeleteCourse(Guid courseId);
    }
}