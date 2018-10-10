using CreamCityCodeFirst.Context;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CreamCityCodeFirst.Repositories;
using CreamCityCodeFirst.Models;

namespace CreamCityCodeFirst.Tests
{
    public class DatabaseTests : IClassFixture<DatabaseTestingFixture<DatabaseTests>>
    {
        DatabaseTestingFixture<DatabaseTests> _fixture;

        public DatabaseTests(DatabaseTestingFixture<DatabaseTests> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task TestStudentCreation()
        {
            var dbContext = _fixture.ServiceProvider.GetService<UniversityDbContext>();

            dbContext.Students.Add(new Student
            {
                BirthDate = new DateTime(2000, 1, 1),
                ExpectedGraduationYear = 2023,
                FirstName = "John",
                LastName = "Doe",
                LastChangedByUser = "test",
                LastChangedTimestamp = DateTimeOffset.UtcNow
            });

            await dbContext.SaveChangesAsync();

            var student = dbContext.Students.FirstOrDefault();

            Assert.NotNull(student);
            Assert.NotEqual(Guid.Empty, student.Id);

        }

        [Fact]
        public async Task TestLocalDbSetChanges()
        {
            Guid studentId;

            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<UniversityDbContext>();

                var studentToAdd = new Student
                {
                    BirthDate = new DateTime(1990, 1, 1),
                    ExpectedGraduationYear = 2008,
                    FirstName = "Update",
                    LastName = "Test",
                    LastChangedByUser = "test",
                    LastChangedTimestamp = DateTimeOffset.UtcNow
                };

                await dbContext.Students.AddAsync(studentToAdd);

                await dbContext.SaveChangesAsync();

                studentId = studentToAdd.Id;
            }

            var testContext = _fixture.ServiceProvider.GetService<UniversityDbContext>();

            var existingStudent = await testContext.Students.FindAsync(studentId);

            Assert.NotNull(existingStudent);

            var studentByName = testContext.Students.Single(s => s.FirstName == "Update" && s.LastName == "Test");

            Assert.Equal(studentId, studentByName.Id);

            studentByName.FirstName = "Local";

            var studentNotFound = testContext.Students.SingleOrDefault(s => s.FirstName == "Local" && s.Id == studentId);

            Assert.Null(studentNotFound);

            var studentFoundinLocal = testContext.Students.Local.SingleOrDefault(s => s.FirstName == "Local" && s.Id == studentId);

            Assert.NotNull(studentFoundinLocal);

            Assert.Equal(studentFoundinLocal.Id, studentByName.Id);

            await testContext.SaveChangesAsync();

            var studentFoundAfterSave = testContext.Students.SingleOrDefault(s => s.FirstName == "Local" && s.Id == studentId);

            Assert.NotNull(studentFoundAfterSave);
        }

        [Fact]
        public async Task TestSoftDeletion()
        {
            var studentId = Guid.NewGuid();

            //Do this in scopes to prove the change tracker isn't faking positives
            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<UniversityDbContext>();

                dbContext.Students.Add(new Student
                {
                    Id = studentId,
                    BirthDate = new DateTime(2000, 1, 1),
                    ExpectedGraduationYear = 2023,
                    FirstName = "John",
                    LastName = "Doe"
                });

                await dbContext.SaveChangesAsync();
            }

            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<UniversityDbContext>();

                var student = dbContext.Students.Find(studentId);

                Assert.NotNull(student);

                student.IsDeleted = true;

                await dbContext.SaveChangesAsync();
            }

            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<UniversityDbContext>();

                var student = dbContext.Students.Find(studentId);

                Assert.Null(student);

                student = dbContext.Students
                    .IgnoreQueryFilters()
                    .Where(s => s.Id == studentId)
                    .Single();

                Assert.NotNull(student);
                Assert.True(student.IsDeleted, "Expected student.IsDeleted == false");

            }
        }

        [Fact]
        public async Task TestCourseCreation()
        {
            var changeHandler = _fixture.ServiceProvider.GetRequiredService<ICourseChangeHandler>();

            var command = new CourseCreationCommand
            {
                ClassDuration = new TimeSpan(1, 30, 0),
                CourseNumber = 100,
                DepartmentId = DepartmentMetaData.MusicDepartmentId,
                Description = "Music Appreciation in the 20th Century",
                StartDate = new DateTimeOffset(new DateTime(2017, 9, 7), new TimeSpan(-5, 0, 0)),
                EndDate = new DateTimeOffset(new DateTime(2017, 12, 20), new TimeSpan(-6, 0, 0)),
                Name = "Music Appreciation",
                Id = Guid.NewGuid(),
                InstructorId = InstructorMetaData.UnassignedInstructorId
            };

            var newCourse = await changeHandler.CreateCourse(command);

            Assert.NotNull(newCourse);

            Assert.Equal(command.ClassDuration, newCourse.ClassDuration);
            Assert.Equal(command.CourseNumber, newCourse.CourseNumber);
            Assert.Equal(command.DepartmentId, newCourse.Department.Id);
            Assert.Equal(command.Description, newCourse.Description);
            Assert.Equal(command.StartDate, newCourse.StartDate);
            Assert.Equal(command.EndDate, newCourse.EndDate);
            Assert.Equal(command.Name, newCourse.Name);
            Assert.Equal(command.Id, newCourse.Id);
            Assert.Equal(command.InstructorId, newCourse.Instructor.Id);
            Assert.NotNull(newCourse.Instructor.Department);

            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<UniversityDbContextReadonly>();

                var found = await dbContext.FindAsync<Course>(newCourse.Id);

                Assert.NotNull(found);
            }

            var repo = _fixture.ServiceProvider.GetRequiredService<ICourseRepository>();

            var summary = (await repo.GetCourses()).FirstOrDefault(c => c.Id == newCourse.Id);

            Assert.NotNull(summary);

            Assert.Equal(90, summary.ClassDurationMinutes);
            Assert.Equal(command.CourseNumber, summary.CourseNumber);
            Assert.Equal(DepartmentMetaData.MusicDepartmentName, summary.DepartmentName);
            Assert.Equal(command.Description, summary.Description);
            Assert.Equal(command.EndDate, summary.EndDate);
            Assert.Equal(command.Id, summary.Id);
            Assert.Contains("Unassigned", summary.InstructorName);
            Assert.Equal(command.Name, summary.Name);
            Assert.Equal(0, summary.NumberStudentsEnrolled);
            Assert.Equal(command.StartDate, summary.StartDate);

        }

        [Fact]
        public async Task TestCourseDeletion()
        {
            var courseId = Guid.NewGuid();

            //first, make a course
            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var changeHandler = scope.ServiceProvider.GetRequiredService<ICourseChangeHandler>();

                var command = new CourseCreationCommand
                {
                    ClassDuration = new TimeSpan(1, 30, 0),
                    CourseNumber = 100,
                    DepartmentId = DepartmentMetaData.MusicDepartmentId,
                    Description = "Music Appreciation in the 20th Century",
                    StartDate = new DateTimeOffset(new DateTime(2017, 9, 7), new TimeSpan(-5, 0, 0)),
                    EndDate = new DateTimeOffset(new DateTime(2017, 12, 20), new TimeSpan(-6, 0, 0)),
                    Name = "Music Appreciation",
                    Id = courseId,
                    InstructorId = InstructorMetaData.UnassignedInstructorId
                };

                var newCourse = await changeHandler.CreateCourse(command);

                Assert.NotNull(newCourse);
            }

            //now, add 1000 students
            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<UniversityDbContext>();

                for(int i = 0; i < 1000; i++)
                {
                    var student = new Student
                    {
                        BirthDate = new DateTime(2000, 1, 1),
                        ExpectedGraduationYear = 2023,
                        FirstName = "John",
                        LastName = $"Doe{i}",
                        LastChangedByUser = "test",
                        LastChangedTimestamp = DateTimeOffset.UtcNow
                    };

                    //add the student
                    dbContext.Students.Add(student);

                    //add student to class
                    dbContext.CourseEnrollments.Add(new CourseEnrollment
                    {
                        CourseId = courseId,
                        FinalGrade = 0m,
                        Student = student,
                        Id = Guid.NewGuid(),
                        Status = EnrollmentStatus.Withdrawn
                    });
                }

                await dbContext.SaveChangesAsync();
            }

            //now, confirm class exists
            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var repo = _fixture.ServiceProvider.GetRequiredService<ICourseRepository>();

                var detail = await repo.GetCourse(courseId);

                Assert.NotNull(detail);

                Assert.Equal(100, detail.CourseNumber);
            }

            //now, confirm class has 1000 students
            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var repo = _fixture.ServiceProvider.GetRequiredService<ICourseRepository>();

                var summary = (await repo.GetCourses()).FirstOrDefault(c => c.Id == courseId);

                Assert.NotNull(summary);

                Assert.Equal(1000, summary.NumberStudentsEnrolled);
            }

            //now, test the view
            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var context = _fixture.ServiceProvider.GetRequiredService<UniversityDbContext>();

                var gpas = context.StudentGPAs.ToList();

                Assert.True(gpas.Count >= 1000);
            }

            //BURN IT DOWN!!!!!
            using (var scope = _fixture.ServiceProvider.CreateScope())
            {
                var changeHandler = scope.ServiceProvider.GetRequiredService<ICourseChangeHandler>();
                var context = _fixture.ServiceProvider.GetRequiredService<UniversityDbContextReadonly>();

                await changeHandler.DeleteCourse(courseId);

                //prove is is gone

                var course = context.Courses.FirstOrDefault(c => c.Id == courseId);

                Assert.Null(course);

                var enrolledStudents = context.CourseEnrollments.Where(c => c.CourseId == courseId).Count();

                Assert.Equal(0, enrolledStudents);

                //but not really :)
                course = context.Courses.IgnoreQueryFilters().FirstOrDefault(c => c.Id == courseId);

                Assert.NotNull(course);

                enrolledStudents = context.CourseEnrollments.IgnoreQueryFilters().Where(c => c.CourseId == courseId).Count();

                Assert.Equal(1000, enrolledStudents);

            }

        }
    }
}
