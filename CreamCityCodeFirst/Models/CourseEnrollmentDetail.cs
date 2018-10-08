using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Models
{
    public class CourseEnrollmentDetail
    {
        public Guid StudentId { get; set; }

        public Decimal FinalGrade { get; set; }

        public StudentDetail Student { get; set; }
    }
}
