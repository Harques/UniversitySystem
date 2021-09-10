using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.LookUp
{
    public class InstructorLookUpDto
    {
        public string Name { get; set; }
        public string Office { get; set; }
        public string Rank { get; set; }
        public string DepartmentName { get; set; }
    }
}