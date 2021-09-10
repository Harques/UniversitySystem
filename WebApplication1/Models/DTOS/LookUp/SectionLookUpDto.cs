using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.LookUp
{
    public class SectionLookUpDto
    {
        public string CourseCode { get; set; }
        public string Semester { get; set; }
        public string InstructorName { get; set; }
    }
}