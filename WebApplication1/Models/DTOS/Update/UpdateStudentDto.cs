using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.Update
{
    public class UpdateStudentDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Advisor { get; set; }
        public string Department { get; set; }
        public string GPA { get; set; }
    }
}