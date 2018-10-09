using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CreamCityCodeFirst.Contracts;
using CreamCityCodeFirst.Models;

namespace CreamCityCodeFirst.Context
{
    /*
     * To Run, in package manager console
     * > dotnet ef //to ensure that ef cli is installed
     * > cd .\CreamCityCodeFirst
     * > dotnet ef migrations add [migration name here] -s ..\CreamCityCodeFirst.Tests\CreamCityCodeFirst.Tests.csproj -v --context UniversityDbContext
     */
    internal partial class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

        protected UniversityDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<StudentGPA> StudentGPAs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(m => m.Instructor) //set up foreign key relationships
                .WithMany(m => m.Courses);

            modelBuilder.Entity<Course>()
                .HasMany(m => m.EnrolledStudents)
                .WithOne(m => m.Course);

            modelBuilder.Entity<Course>()
                .HasOne(m => m.Department);

            modelBuilder.Entity<Instructor>()
                .HasOne(m => m.Department);

            modelBuilder.Entity<Student>()
                .HasMany(m => m.EnrolledCourses)
                .WithOne(m => m.Student);

            modelBuilder.Entity<CourseEnrollment>()
                .Property(m => m.FinalGrade)
                .HasColumnType("decimal(6,3)");

            modelBuilder.Entity<Department>()
                .Property(d => d.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<StudentGPA>()
                .HasKey(s => s.Id);


            ApplyCommonStructure(modelBuilder);

            SetupSeedData(modelBuilder);

        }

        private void SetupSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = DepartmentMetaData.EducationDepartmentId,
                    Code = DepartmentMetaData.EducationDepartmentCode,
                    Description = DepartmentMetaData.EducationDepartmentName,
                    Name = DepartmentMetaData.EducationDepartmentName,
                    LastChangedByUser = "system startup",
                    LastChangedTimestamp = DateTimeOffset.UtcNow
                },
                new Department
                {
                    Id = DepartmentMetaData.MusicDepartmentId,
                    Code = DepartmentMetaData.MusicDepartmentCode,
                    Description = DepartmentMetaData.MusicDepartmentName,
                    Name = DepartmentMetaData.MusicDepartmentName,
                    LastChangedByUser = "system startup",
                    LastChangedTimestamp = DateTimeOffset.UtcNow
                },
                new Department
                {
                    Id = DepartmentMetaData.EngineeringDepartmentId,
                    Code = DepartmentMetaData.EngineeringDepartmentCode,
                    Description = DepartmentMetaData.EngineeringDepartmentName,
                    Name = DepartmentMetaData.EngineeringDepartmentName,
                    LastChangedByUser = "system startup",
                    LastChangedTimestamp = DateTimeOffset.UtcNow
                },
                new Department
                {
                    Id = DepartmentMetaData.UnassignedDepartmentId,
                    Code = DepartmentMetaData.UnassignedDepartmentCode,
                    Description = DepartmentMetaData.UnassignedDepartmentName,
                    Name = DepartmentMetaData.UnassignedDepartmentName,
                    LastChangedByUser = "system startup",
                    LastChangedTimestamp = DateTimeOffset.UtcNow
                });

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    Id = InstructorMetaData.UnassignedInstructorId,
                    BirthDate = new DateTime(1900, 1, 1),
                    FirstName = "Unassigned",
                    LastName = "Unassigned",
                    HireDate = new DateTime(1900, 1, 1),
                    DepartmentId = DepartmentMetaData.UnassignedDepartmentId,
                    LastChangedByUser = "system startup",
                    LastChangedTimestamp = DateTimeOffset.UtcNow
                });
        }

        private void ApplyCommonStructure(ModelBuilder modelBuilder)
        {
            var calculationBaseType = typeof(AuditableEntity);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!calculationBaseType.IsAssignableFrom(entityType.ClrType))
                {
                    continue;
                }

                modelBuilder.Entity(entityType.Name)
                    .HasIndex(nameof(AuditableEntity.IsDeleted));

                LambdaExpression filterExpression = GetNotDeletedFilter(entityType);

                modelBuilder.Entity(entityType.Name)
                    .HasQueryFilter(filterExpression);
            }
        }

        /// <summary>
        /// Creates a type specific expression that equates to "t => !t.IsDeleted;"
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private static LambdaExpression GetNotDeletedFilter(IMutableEntityType entityType)
        {
            var parameter = Expression.Parameter(entityType.ClrType);

            var isDeletedProperty = Expression.Property(parameter, nameof(AuditableEntity.IsDeleted));

            var negation = Expression.Not(isDeletedProperty);

            var filterExpression = Expression.Lambda(negation, parameter);
            return filterExpression;
        }

        public override int SaveChanges()
        {
            InspectChangesAndApplyAudits();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            InspectChangesAndApplyAudits();

            return await base.SaveChangesAsync();
        }

        private void InspectChangesAndApplyAudits()
        {
            foreach (var change in ChangeTracker.Entries())
            {
                if (!(change.Entity is AuditableEntity))
                    continue;

                if (change.State == EntityState.Added
                    || change.State == EntityState.Modified)
                {
                    var entity = change.Entity as AuditableEntity;

                    entity.LastChangedByUser = "test";
                    entity.LastChangedTimestamp = DateTimeOffset.UtcNow;
                }
            }
        }
    }
}
