using System;
using CreamCityCodeFirst.Context;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreamCityCodeFirst.Migrations
{
    public partial class course_average_function : Migration
    {

        private const string CreateCourseAverageV1 = @"CREATE FUNCTION [dbo].[" + UniversityDbContext.CourseAverageFunctionName + @"]()
RETURNS TABLE
AS
RETURN
(
    SELECT c." + nameof(Course.Id) + @" as " + nameof(CourseAverage.CourseId) + @", 
            AVG(ce." + nameof(CourseEnrollment.FinalGrade) + @") as " + nameof(CourseAverage.AverageGrade) + @"
    FROM " + nameof(UniversityDbContext.Courses) + @" c
    LEFT OUTER JOIN " + nameof(UniversityDbContext.CourseEnrollments) + @" ce 
        on c." + nameof(Course.Id) + @" = ce." + nameof(CourseEnrollment.CourseId) + @"
    GROUP BY c." + nameof(Course.Id) + @"
)";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(CreateCourseAverageV1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION " + UniversityDbContext.CourseAverageFunctionName);
        }
    }
}
