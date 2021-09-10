using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.LookUp
{
    public class StudentLookUpDto
    {
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public long Number { get; set; }

        public double GPA { get; set; }

        public string InstructorName { get; set; }
    }
}