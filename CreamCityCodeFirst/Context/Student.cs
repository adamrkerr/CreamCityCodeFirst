using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Context
{
    internal class Student : PersonBase
    {
        public Student() : base()
        {
            EnrolledCourses = new List<CourseEnrollment>();
        }

        public int ExpectedGraduationYear { get; set; }

        public IList<CourseEnrollment> EnrolledCourses { get; set; }
    }
}
