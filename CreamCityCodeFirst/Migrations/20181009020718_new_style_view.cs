using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreamCityCodeFirst.Migrations
{
    public partial class new_style_view : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //This was never really a table, and now we can use the DbQuery type
            //migrationBuilder.DropTable(
            //    name: "StudentGPAs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //This was never really a table, and now we can use the DbQuery type
            //migrationBuilder.CreateTable(
            //    name: "StudentGPAs",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false),
            //        CoursesEnrolled = table.Column<int>(nullable: false),
            //        GPA = table.Column<decimal>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StudentGPAs", x => x.Id);
            //    });

        }
    }
}
