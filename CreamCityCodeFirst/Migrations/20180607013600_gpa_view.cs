using CreamCityCodeFirst.Context;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreamCityCodeFirst.Migrations
{
    public partial class gpa_view : Migration
    {
        internal static string StudentGPAViewV1 = $@"
            CREATE VIEW {nameof(UniversityDbContext.StudentGPAs)} AS
                SELECT s.Id as {nameof(StudentGPA.Id)}, count(c.{nameof(CourseEnrollment.Id)}) as {nameof(StudentGPA.CoursesEnrolled)},  avg(c.{nameof(CourseEnrollment.FinalGrade)}) as {nameof(StudentGPA.GPA)}
                From {nameof(UniversityDbContext.Students)} s
                Left outer join {nameof(UniversityDbContext.CourseEnrollments)} c on c.{nameof(CourseEnrollment.StudentId)} = s.{nameof(Student.Id)}
                Group by s.{nameof(Student.Id)}";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(StudentGPAViewV1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP VIEW {nameof(UniversityDbContext.StudentGPAs)}");
        }
    }
}
