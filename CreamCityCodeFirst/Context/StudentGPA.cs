using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Context
{
    internal class StudentGPA: IIdentified<Guid>
    {
        public Guid Id { get; set; }

        public decimal? GPA { get; set; }

        public int CoursesEnrolled { get; set; }
    }
}
