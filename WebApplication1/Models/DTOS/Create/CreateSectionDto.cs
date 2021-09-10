using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.Create
{
    public class CreateSectionDto
    {
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string Instructor { get; set; }

    }
}