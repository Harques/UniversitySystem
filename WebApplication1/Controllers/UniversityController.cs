using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using WebApplication1.Models;
using WebApplication1.Models.DTOS;
using WebApplication1.Models.DTOS.Update;
using WebApplication1.Models.DTOS.Create;
using WebApplication1.Models.DTOS.LookUp;
using AutoMapper;
using WebApplication1.Businesses;

namespace WebApplication1.Controllers
{

    public class UniversityController : ApiController
    {
        private UniversityEntities entities;
        private Mapper mapper;
        private StudentBusinessService studentBusinessService;
        public UniversityController()
        {
            studentBusinessService = new StudentBusinessService();
            entities = new UniversityEntities();
            var config = new MapperConfiguration(cfg => {cfg.CreateMap<CreateStudentDto, STUDENT>()
                .ForMember(dest => dest.student_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.student_number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.advisor_id, opt => opt.MapFrom(src => src.advisor_id))
                .ForMember(dest => dest.department_id, opt => opt.MapFrom(src => src.department_id))
                .ForMember(dest => dest.gpa, opt => opt.MapFrom(src => src.gpa))
                .ForAllOtherMembers(opts => opts.Ignore());
            });
            mapper = new Mapper(config);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("students")]
        public IHttpActionResult CreateStudent(CreateStudentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(studentBusinessService.CreateStudentService(dto));
        }
        [Authorize(Roles = "Admin")]
        [ActionName("students")]
        [HttpGet]
        public IHttpActionResult ListStudent()
        {
            var list = entities.STUDENTs.Include("DEPARTMENT").Select(x => new StudentLookUpDto
            {
                Name = x.student_name,
                DepartmentName = x.DEPARTMENT.department_name,
                GPA = x.gpa,
                InstructorName =
                entities.INSTRUCTORs.Where(m => m.id == x.advisor_id).Select(m => m.instructor_name).FirstOrDefault(),
                Number = x.student_number
            }).ToList();
            return Json(list);

        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("students")]
        [HttpGet]
        public StudentLookUpDto GetStudent(int id)
        {
            var student = entities.STUDENTs.Find(id);
            var studentDto = new StudentLookUpDto
            {
                Name = student.student_name,
                DepartmentName = student.DEPARTMENT.department_name,
                GPA = student.gpa,
                InstructorName = entities.INSTRUCTORs.Where(m => m.id == student.advisor_id).Select(m => m.instructor_name).FirstOrDefault(),
                Number = student.student_number
            };
            return studentDto;
        }

        [ActionName("students")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult UpdateStudent(UpdateStudentDto dto) {
            var student = entities.STUDENTs.Find(int.Parse(dto.ID));

            student.student_name = dto.Name == "" ? student.student_name : dto.Name;
            student.student_number = dto.Number == "" ? student.student_number : long.Parse(dto.Number);
            student.advisor_id = dto.Advisor == "" ? student.advisor_id : int.Parse(dto.Advisor);
            student.department_id = dto.Department == "" ? student.department_id : int.Parse(dto.Department);
            student.gpa = dto.GPA == "" ? student.gpa : double.Parse(dto.GPA);

            System.Diagnostics.Debug.WriteLine("Entered");
            System.Diagnostics.Debug.WriteLine(dto.ID);
            System.Diagnostics.Debug.WriteLine(dto);
            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("students")]
        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            var student = entities.STUDENTs.Find(id);
            entities.STUDENTs.Remove(student);
            entities.SaveChanges();
            System.Diagnostics.Debug.WriteLine(id);
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("sections")]
        [HttpGet]
        public IEnumerable<SectionLookUpDto> ListSection()
        {
            return entities.SECTIONs.Select(x => new SectionLookUpDto { CourseCode = entities.COURSEs.Where(m => m.id == x.course_id).Select(m => m.course_code).FirstOrDefault(),
                InstructorName = entities.INSTRUCTORs.Where(m => m.id == x.instructor_id).Select(m => m.instructor_name).FirstOrDefault(), Semester = x.semester }).ToList();
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("sections")]
        [HttpGet]
        public SectionLookUpDto GetSection(int id)
        {
            var section = entities.SECTIONs.Find(id);
            var sectionDto = new SectionLookUpDto { InstructorName = entities.INSTRUCTORs.Where(m => m.id == section.instructor_id).Select(m => m.instructor_name).FirstOrDefault(),
                CourseCode = entities.COURSEs.Where(m => m.id == section.course_id).Select(m => m.course_code).FirstOrDefault(), Semester = section.semester };
            return sectionDto;
        }

        [Authorize(Roles = "Admin")]
        [ActionName("sections")]
        [HttpPost]
        public IHttpActionResult CreateSection(CreateSectionDto dto)
        {
            var courseID = entities.COURSEs.Where(x => x.course_name == dto.CourseName).Select(x => x.id).FirstOrDefault();
            SECTION sct = new SECTION { course_id = courseID, 
                instructor_id = entities.INSTRUCTORs.Where(m => m.instructor_name == dto.Instructor).Select(m => m.id).FirstOrDefault(),
                semester = dto.Semester};

            entities.SECTIONs.Add(sct);
            entities.SaveChanges();
            return Json("Worked Succesfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("sections")]
        [HttpPut]
        public IHttpActionResult UpdateSection (UpdateSectionDto dto)
        {
            var section = entities.SECTIONs.Find(int.Parse(dto.ID));

            section.course_id = dto.CourseID == "" ? section.course_id : int.Parse(dto.CourseID);
            section.semester = dto.Semester == "" ? section.semester : dto.Semester;
            section.instructor_id = dto.Instructor == "" ? section.instructor_id : int.Parse(dto.Instructor);

            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("sections")]
        [HttpDelete]
        public IHttpActionResult DeleteSection(int id)
        {
            var section = entities.SECTIONs.Find(id);
            entities.SECTIONs.Remove(section);
            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("student-sections")]
        [HttpGet]
        public IEnumerable<StudentSectionLookUpDto> ListStudentSection()
        {

            return entities.STUDENT_SECTIONS.Include("SECTION.COURSE").Select(x => new StudentSectionLookUpDto { CourseName = x.SECTION.COURSE.course_name, 
                Number = entities.STUDENTs.Where(m => m.id == x.student_id).Select(m => m.student_number).FirstOrDefault(), 
                Grade = x.grade, Semester = x.SECTION.semester }).ToList();
        }
        [Authorize(Roles = "User, Admin")]
        [ActionName("student-sections")]
        [HttpGet]
        public StudentSectionLookUpDto GetStudentSection(int id, int id2)
        {
            var studentSectionDto = entities.STUDENT_SECTIONS.Include("SECTION.COURSE").Where(x => x.section_id == id && x.student_id == id2)
                .Select(x => new StudentSectionLookUpDto { CourseName = x.SECTION.COURSE.course_name, Number = entities.STUDENTs.Where(m => m.id == x.student_id).Select(m => m.student_number).FirstOrDefault(), Grade = x.grade, Semester = x.SECTION.semester })
                .FirstOrDefault();
            return studentSectionDto;
        }

        [Authorize(Roles = "Admin")]
        [ActionName("student-sections")]
        [HttpPost]
        public IHttpActionResult CreateStudentSection(CreateStudentSectionDto dto)
        {
            var std = entities.STUDENTs.Where(x => x.student_number == long.Parse(dto.Number)).FirstOrDefault<STUDENT>();
            var sct = entities.SECTIONs.Where(x => x.id == dto.ID).FirstOrDefault<SECTION>();
            STUDENT_SECTIONS stdSct = new STUDENT_SECTIONS
            {
                student_id = std.id,
                section_id = sct.id,
                STUDENT = std,
                SECTION = sct,
                grade = ""
            };

            entities.STUDENT_SECTIONS.Add(stdSct);
            entities.SaveChanges();
            return Json("Worked Succesfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("student-sections")]
        [HttpPut]
        public IHttpActionResult UpdateStudentSection(UpdateStudentSectionDto dto)
        {
            var studentSection = entities.STUDENT_SECTIONS.Find(int.Parse(dto.ID));
            
            studentSection.student_id = dto.StudentID == "" ? studentSection.student_id : int.Parse(dto.StudentID);
            studentSection.section_id = dto.SectionID == "" ? studentSection.section_id : int.Parse(dto.SectionID);
            studentSection.grade = dto.Grade == "" ? studentSection.grade : dto.Grade;

            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("student-sections")]
        [HttpDelete]
        public IHttpActionResult DeleteStudentSection(int id)
        {
            var studentSection = entities.STUDENT_SECTIONS.Find(id);
            entities.STUDENT_SECTIONS.Remove(studentSection);
            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("departments")]
        [HttpGet]
        public IEnumerable<DepartmentLookUpDto> ListDepartments()
        {

            return entities.DEPARTMENTs.Select(x => new DepartmentLookUpDto
            {
                Name = x.department_name
            }).ToList();
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("departments")]
        [HttpGet]
        public DepartmentLookUpDto GetDepartments(int id)
        {
            var department = entities.DEPARTMENTs.Find(id);
            var departmentDto = new DepartmentLookUpDto { Name = department.department_name };
            return departmentDto;
        }

        [Authorize(Roles = "Admin")]
        [ActionName("departments")]
        [HttpPost]
        public IHttpActionResult CreateDepartment(CreateDepartmentDto dto)
        {

            DEPARTMENT dpt = new DEPARTMENT
            {
                department_name = dto.Name
            };

            entities.DEPARTMENTs.Add(dpt);
            entities.SaveChanges();
            return Json("Worked Succesfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("departments")]
        [HttpPut]
        public IHttpActionResult UpdateDepartment(UpdateDepartmentDto dto)
        {
            var department = entities.DEPARTMENTs.Find(int.Parse(dto.ID));
            entities.DEPARTMENTs.Remove(department);
            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("departments")]
        [HttpDelete]
        public IHttpActionResult DeleteDepartment(int id)
        {
            var department = entities.DEPARTMENTs.Find(id);
            entities.DEPARTMENTs.Remove(department);
            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("courses")]
        [HttpGet]
        public IEnumerable<CourseLookUpDto> ListCourse()
        {
            return entities.COURSEs.Include("DEPARTMENT").Select(x => new CourseLookUpDto { Name = x.course_name, CreditHours = (int)x.credit_hours, DepartmentName = x.DEPARTMENT.department_name, Code = entities.COURSEs.Where(m => m.id == x.id).Select(m => m.course_code).FirstOrDefault() }).ToList();
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("courses")]
        [HttpGet]
        public CourseLookUpDto GetCourse(int id)
        {
            var courseDto = entities.COURSEs.Include("DEPARTMENT").Where(x => x.id == id).Select(x => new CourseLookUpDto { CreditHours = (int)x.credit_hours, DepartmentName = x.DEPARTMENT.department_name, Code = entities.COURSEs.Where(m => m.id == x.id).Select(m => m.course_code).FirstOrDefault(), Name = x.course_name }).FirstOrDefault();
            return courseDto;
        }

        [Authorize(Roles = "Admin")]
        [ActionName("courses")]
        [HttpPost]
        public IHttpActionResult CreateCourse(CreateCourseDto dto)
        {
            var department = entities.DEPARTMENTs.Where(x => x.department_name == dto.DepartmentName)
                .FirstOrDefault<DEPARTMENT>();
            COURSE crs = new COURSE
            {
                course_code = dto.Code,
                course_name = dto.Name,
                course_description = dto.Description,
                credit_hours = dto.CreditHours,
                department_id = department.id,
                DEPARTMENT = department
            };

            entities.COURSEs.Add(crs);
            entities.SaveChanges();
            return Json("Worked Succesfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("courses")]
        [HttpPut]
        public IHttpActionResult UpdateCourse(UpdateCourseDto dto)
        {
            var course = entities.COURSEs.Find(int.Parse(dto.ID));
            course.course_description = dto.Description == "" ? course.course_description : dto.Description;
            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("courses")]
        [HttpDelete]
        public IHttpActionResult DeleteCourse(int id)
        {
            var course = entities.COURSEs.Find(id);
            entities.COURSEs.Remove(course);
            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("instructors")]
        [HttpGet]
        public IEnumerable<InstructorLookUpDto> ListInstructors()
        {
            return entities.INSTRUCTORs.Include("DEPARTMENT").Select(x => new InstructorLookUpDto { DepartmentName = x.DEPARTMENT.department_name, Name = x.instructor_name, Office = x.instructor_office, Rank = x.rank }).ToList();
        }

        [Authorize(Roles = "User, Admin")]
        [ActionName("instructors")]
        [HttpGet]
        public InstructorLookUpDto GetInstructor(string id)
        {
            var instructorDto = entities.INSTRUCTORs.Include("DEPARTMENT").Where(x => x.instructor_name == id).Select(x => new InstructorLookUpDto { DepartmentName = x.DEPARTMENT.department_name, Name = x.instructor_name, Office = x.instructor_office, Rank = x.rank }).FirstOrDefault();
            return instructorDto;
        }

        [Authorize(Roles = "Admin")]
        [ActionName("instructors")]
        [HttpPost]
        public IHttpActionResult CreateInstructor(CreateInstructorDto dto)
        {
            var department = entities.DEPARTMENTs.Where(x => x.department_name == dto.DepartmentName)
                .FirstOrDefault<DEPARTMENT>();
            INSTRUCTOR ins = new INSTRUCTOR
            {
                instructor_name = dto.Name,
                instructor_office = dto.Office,
                rank = dto.Rank,
                department_id = department.id,
                DEPARTMENT = department
            };

            entities.INSTRUCTORs.Add(ins);
            entities.SaveChanges();
            return Json("Worked Succesfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("instructors")]
        [HttpPut]
        public IHttpActionResult UpdateInstructor(UpdateInstructorDto dto)
        {
            var instructor = entities.INSTRUCTORs.Find(int.Parse(dto.ID));
            instructor.instructor_name = dto.Name == "" ? instructor.instructor_name : dto.Name;
            instructor.instructor_office = dto.Office == "" ? instructor.instructor_office : dto.Office;
            instructor.rank = dto.Rank == "" ? instructor.rank : dto.Rank;
            instructor.department_id = dto.DepartmentID == "" ? instructor.department_id : int.Parse(dto.DepartmentID);
            entities.SaveChanges();
            return Json("Worked Successfully.");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("instructors")]
        [HttpDelete]
        public IHttpActionResult DeleteInstructor(int id)
        {
            var instructor = entities.INSTRUCTORs.Find(id);
            entities.INSTRUCTORs.Remove(instructor);
            entities.SaveChanges();

            return Json("Worked Successfully.");
        }

    }
}
