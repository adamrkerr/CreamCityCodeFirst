using Microsoft.EntityFrameworkCore;
using CreamCityCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreamCityCodeFirst.Context
{
    internal static class UniversityDbContextSeedExtension
    {
        public static bool AllMigrationsApplied(this UniversityDbContext context)
        {
            return !context.Database.GetPendingMigrations().Any();
        }

        public static async Task EnsureSeedData(this UniversityDbContext context)
        {
            if (!context.AllMigrationsApplied())
            {
                return;
            }

            if (!context.Departments.Any())
            {
                await context.Departments.AddAsync(new Department
                {
                    Id = DepartmentMetaData.EducationDepartmentId,
                    Code = DepartmentMetaData.EducationDepartmentCode,
                    Description = DepartmentMetaData.EducationDepartmentName,
                    Name = DepartmentMetaData.EducationDepartmentName
                });

                await context.Departments.AddAsync(new Department
                {
                    Id = DepartmentMetaData.MusicDepartmentId,
                    Code = DepartmentMetaData.MusicDepartmentCode,
                    Description = DepartmentMetaData.MusicDepartmentName,
                    Name = DepartmentMetaData.MusicDepartmentName
                });

                await context.Departments.AddAsync(new Department
                {
                    Id = DepartmentMetaData.EngineeringDepartmentId,
                    Code = DepartmentMetaData.EngineeringDepartmentCode,
                    Description = DepartmentMetaData.EngineeringDepartmentName,
                    Name = DepartmentMetaData.EngineeringDepartmentName
                });

                await context.Departments.AddAsync(new Department
                {
                    Id = DepartmentMetaData.UnassignedDepartmentId,
                    Code = DepartmentMetaData.UnassignedDepartmentCode,
                    Description = DepartmentMetaData.UnassignedDepartmentName,
                    Name = DepartmentMetaData.UnassignedDepartmentName
                });

                await context.SaveChangesAsync();
            }

            if (!context.Instructors.Any())
            {
                await context.Instructors.AddAsync(new Instructor
                {
                    Id = InstructorMetaData.UnassignedInstructorId,
                    BirthDate = new DateTime(1900, 1, 1),
                    FirstName = "Unassigned",
                    LastName = "Unassigned",
                    HireDate = new DateTime(1900, 1, 1),
                    DepartmentId = DepartmentMetaData.UnassignedDepartmentId
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
