using CreamCityCodeFirst.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace CreamCityCodeFirst.Context
{
    internal class CourseEnrollment : AuditableEntity<Guid>
    {
        public CourseEnrollment() : base()
        {

        }

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        public Decimal FinalGrade { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }

    }
}
