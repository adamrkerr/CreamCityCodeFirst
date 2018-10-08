using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Models
{
    public class InstructorDetail
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Title { get; set; }

        public DateTime HireDate { get; set; }
                
        public DepartmentDetail Department { get; set; }
    }
}
