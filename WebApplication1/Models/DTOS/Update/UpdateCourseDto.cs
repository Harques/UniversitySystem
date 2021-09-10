using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.Update
{
    public class UpdateCourseDto
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CreditHours { get; set; }
        public string DepartmentID { get; set; }
    }
}