using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.Create
{
    public class CreateCourseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public string DepartmentName { get; set; }
    }
}