//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class STUDENT_SECTIONS
    {
        public int student_id { get; set; }
        public int section_id { get; set; }
        public string grade { get; set; }
        public int id { get; set; }
    
        public virtual SECTION SECTION { get; set; }
        public virtual STUDENT STUDENT { get; set; }
    }
}
