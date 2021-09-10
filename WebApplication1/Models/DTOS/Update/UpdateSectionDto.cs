using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.Update
{
    public class UpdateSectionDto
    {
        public string ID { get; set; }
        public string CourseID { get; set; }
        public string Semester { get; set; }
        public string Instructor { get; set; }
    }
}