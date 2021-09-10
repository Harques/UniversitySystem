using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTOS.Create
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Name field is mandatory.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Number field is mandatory.")]
        public string Number { get; set; }
        public string Department { get; set; }
        public string Advisor { get; set; }
        public int department_id { get; set; }
        public int advisor_id { get; set; }

        public int gpa = 0;


    }
}