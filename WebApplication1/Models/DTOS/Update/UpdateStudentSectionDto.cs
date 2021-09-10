using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.Update
{
    public class UpdateStudentSectionDto
    {
        public string ID { get; set; }
        public string StudentID { get; set; }
        public string SectionID { get; set; }
        public string Grade { get; set; }

    }
}