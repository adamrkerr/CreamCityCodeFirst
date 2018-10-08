using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.Repositories
{
    public class CourseCreationCommand
    {
        public Guid Id { get; set; }

        public int CourseNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public TimeSpan? ClassDuration { get; set; }

        public Guid? InstructorId { get; set; } 

        public int? DepartmentId { get; set; }
    }
}
