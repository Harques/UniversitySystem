using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.LookUp
{
    public class StudentSectionLookUpDto
    {
        public long Number { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string Grade { get; set; }

    }
}