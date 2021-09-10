using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.LookUp
{
    public class CourseLookUpDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public string DepartmentName { get; set; }
    }
}