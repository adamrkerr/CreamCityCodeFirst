﻿// <auto-generated />
using System;
using CreamCityCodeFirst.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CreamCityCodeFirst.Migrations
{
    [DbContext(typeof(UniversityDbContext))]
    [Migration("20181009012233_new_style_seed")]
    partial class new_style_seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CreamCityCodeFirst.Context.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan?>("ClassDuration");

                    b.Property<int?>("CourseNumber")
                        .IsRequired();

                    b.Property<int?>("DepartmentId")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<DateTimeOffset?>("EndDate");

                    b.Property<Guid?>("InstructorId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastChangedByUser")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("LastChangedTimestamp")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CreamCityCodeFirst.Context.CourseEnrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CourseId");

                    b.Property<decimal>("FinalGrade")
                        .HasColumnType("decimal(6,3)");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastChangedByUser")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("LastChangedTimestamp")
                        .IsRequired();

                    b.Property<Guid>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseEnrollments");
                });

            modelBuilder.Entity("CreamCityCodeFirst.Context.Department", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastChangedByUser")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("LastChangedTimestamp")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Departments");

                    b.HasData(
                        new { Id = 3, Code = "EDU", Description = "Education", IsDeleted = false, LastChangedByUser = "system startup", LastChangedTimestamp = new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 917, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "Education" },
                        new { Id = 1, Code = "MSC", Description = "Music", IsDeleted = false, LastChangedByUser = "system startup", LastChangedTimestamp = new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 917, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "Music" },
                        new { Id = 2, Code = "ENG", Description = "Engineering", IsDeleted = false, LastChangedByUser = "system startup", LastChangedTimestamp = new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 917, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "Engineering" },
                        new { Id = 4, Code = "UNX", Description = "Unassigned", IsDeleted = false, LastChangedByUser = "system startup", LastChangedTimestamp = new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 917, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "Unassigned" }
                    );
                });

            modelBuilder.Entity("CreamCityCodeFirst.Context.Instructor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired();

                    b.Property<int?>("DepartmentId")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<DateTime?>("HireDate")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastChangedByUser")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("LastChangedTimestamp")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Instructors");

                    b.HasData(
                        new { Id = new Guid("e2c8de27-9214-4309-95c5-d15837ce064a"), BirthDate = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), DepartmentId = 4, FirstName = "Unassigned", HireDate = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), IsDeleted = false, LastChangedByUser = "system startup", LastChangedTimestamp = new DateTimeOffset(new DateTime(2018, 10, 9, 1, 22, 32, 919, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), LastName = "Unassigned" }
                    );
                });

            modelBuilder.Entity("CreamCityCodeFirst.Context.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired();

                    b.Property<int>("ExpectedGraduationYear");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastChangedByUser")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("LastChangedTimestamp")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CreamCityCodeFirst.Context.StudentGPA", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CoursesEnrolled");

                    b.Property<decimal?>("GPA");

                    b.HasKey("Id");

                    b.ToTable("StudentGPAs");
                });

            modelBuilder.Entity("CreamCityCodeFirst.Context.Course", b =>
                {
                    b.HasOne("CreamCityCodeFirst.Context.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CreamCityCodeFirst.Context.Instructor", "Instructor")
                        .WithMany("Courses")
                        .HasForeignKey("InstructorId");
                });

            modelBuilder.Entity("CreamCityCodeFirst.Context.CourseEnrollment", b =>
                {
                    b.HasOne("CreamCityCodeFirst.Context.Course", "Course")
                        .WithMany("EnrolledStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CreamCityCodeFirst.Context.Student", "Student")
                        .WithMany("EnrolledCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CreamCityCodeFirst.Context.Instructor", b =>
                {
                    b.HasOne("CreamCityCodeFirst.Context.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
