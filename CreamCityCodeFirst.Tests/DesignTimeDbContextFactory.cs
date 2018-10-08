using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CreamCityCodeFirst.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Tests
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UniversityDbContext>
    {
        private const string DefaultConnection = "Server=(localdb)\\mssqllocaldb;Database=UniversityMigration;Trusted_Connection=True;MultipleActiveResultSets=true";

        public UniversityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UniversityDbContext>();

            builder.UseSqlServer(DefaultConnection);

            return new UniversityDbContext(builder.Options);
        }
    }
}
