using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreamCityCodeFirst.Migrations
{
    public partial class new_style_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Description", "IsDeleted", "LastChangedByUser", "LastChangedTimestamp", "Name" },
                values: new object[,]
                {
                    { 3, "EDU", "Education", false, "system startup", new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 917, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Education" },
                    { 1, "MSC", "Music", false, "system startup", new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 917, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Music" },
                    { 2, "ENG", "Engineering", false, "system startup", new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 917, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Engineering" },
                    { 4, "UNX", "Unassigned", false, "system startup", new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 917, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Unassigned" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "FirstName", "HireDate", "IsDeleted", "LastChangedByUser", "LastChangedTimestamp", "LastName", "MiddleName", "Title" },
                values: new object[] { new Guid("e2c8de27-9214-4309-95c5-d15837ce064a"), new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Unassigned", new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "system startup", new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 919, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Unassigned", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: new Guid("e2c8de27-9214-4309-95c5-d15837ce064a"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
