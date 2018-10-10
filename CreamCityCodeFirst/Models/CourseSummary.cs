using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Models
{
    public class CourseSummary
    {
        public Guid Id { get; set; }

        public string DepartmentName { get; set; }
        
        public int CourseNumber { get; set; }

        public int NumberStudentsEnrolled { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public int ClassDurationMinutes { get; set; }

        public string InstructorName { get; set; }

    }
}
