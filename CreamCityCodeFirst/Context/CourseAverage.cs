using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Context
{
    internal class CourseAverage
    {
        public Guid CourseId { get; set; }

        public decimal? AverageGrade { get; set; }

        //Query types can have navigation properties
        public Course Course { get; set; }
    }
}
