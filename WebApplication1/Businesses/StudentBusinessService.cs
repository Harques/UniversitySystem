using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.Models.DTOS.Create;

namespace WebApplication1.Businesses
{

    public class StudentBusinessService
    {
        private UniversityEntities entities;
        private Mapper mapper;
        
        public StudentBusinessService()
        {
            entities = new UniversityEntities();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CreateStudentDto, STUDENT>()
                .ForMember(dest => dest.student_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.student_number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.advisor_id, opt => opt.MapFrom(src => src.advisor_id))
                .ForMember(dest => dest.department_id, opt => opt.MapFrom(src => src.department_id))
                .ForMember(dest => dest.gpa, opt => opt.MapFrom(src => src.gpa))
                .ForAllOtherMembers(opts => opts.Ignore());
            });
            mapper = new Mapper(config);
        }
        public Object CreateStudentService(CreateStudentDto dto)
        {
            try
            {
                var stdDepartment = entities.DEPARTMENTs.Where(x => x.department_name == dto.Department).FirstOrDefault<DEPARTMENT>();
                var stdInstructor = entities.INSTRUCTORs.Where(x => x.instructor_name == dto.Advisor).FirstOrDefault<INSTRUCTOR>();

                dto.department_id = stdDepartment.id;
                dto.advisor_id = stdInstructor.id;

                var std = mapper.Map<STUDENT>(dto);
                entities.STUDENTs.Add(std);
                entities.SaveChanges();
            }
            catch (Exception e)
            {
                return e;
            }
            return "Worked Successfully.";
        }
    }
}