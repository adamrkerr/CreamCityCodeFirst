using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Models
{
    public class CourseDetail
    {
        public CourseDetail()
        {
            EnrolledStudents = new List<CourseEnrollmentDetail>();
        }

        public Guid Id { get; set; }
        
        public int CourseNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public TimeSpan? ClassDuration { get; set; }
                
        public InstructorDetail Instructor { get; set; }

        public DepartmentDetail Department { get; set; }

        public IList<CourseEnrollmentDetail> EnrolledStudents { get; set; }

    }
}
