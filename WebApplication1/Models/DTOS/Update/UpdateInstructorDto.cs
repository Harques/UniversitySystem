using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.Update
{
    public class UpdateInstructorDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }
        public string Rank { get; set; }
        public string DepartmentID { get; set; }
    }
}