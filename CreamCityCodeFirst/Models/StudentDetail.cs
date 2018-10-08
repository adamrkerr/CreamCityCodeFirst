using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Models
{
    public class StudentDetail
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public int ExpectedGraduationYear { get; set; }
    }
}
