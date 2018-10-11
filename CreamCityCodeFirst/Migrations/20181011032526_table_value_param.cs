using System;
using CreamCityCodeFirst.Context;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreamCityCodeFirst.Migrations
{
    public partial class table_value_param : Migration
    {
        private const string CreateCourseNumberTableType = 
@"CREATE TYPE " + UniversityDbContext.CourseNumberTableTypeName + @" AS TABLE
( CourseNumber int NOT NULL )";

        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(CreateCourseNumberTableType);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP TYPE {UniversityDbContext.CourseNumberTableTypeName}");
        }
    }
}
