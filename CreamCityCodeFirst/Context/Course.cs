using CreamCityCodeFirst.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreamCityCodeFirst.Context
{
    internal class Course : AuditableEntity<Guid>
    {
        public Course() : base()
        {
            EnrolledStudents = new List<CourseEnrollment>();
        }

        [Required]
        public int? CourseNumber { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public TimeSpan? ClassDuration { get; set; }

        public Guid? InstructorId { get; set; } //if you name it <Class>Id, it automatically becomes a foreign key

        public Instructor Instructor { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public IList<CourseEnrollment> EnrolledStudents{ get; set; }
    }
}
