using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CreamCityCodeFirst.Context
{
    internal class Instructor : PersonBase
    {
        public Instructor() : base()
        {
            Courses = new List<Course>();
        }

        public string Title { get; set; }

        [Required]
        public DateTime? HireDate { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        public IList<Course> Courses { get; set; }

        public Department Department { get; set; }
    }
}
