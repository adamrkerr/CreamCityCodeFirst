using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreamCityCodeFirst.Migrations
{
    public partial class type_conversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CourseEnrollments",
                nullable: false,
                defaultValue: "");           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CourseEnrollments");
            
        }
    }
}
