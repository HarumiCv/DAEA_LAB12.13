using LAB14.Data;
using LAB14.Models;
using LAB14.Models.Requests;
using LAB14.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB14.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public StudentsCustomController(DemoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void Create([FromBody] UserRequestV1 request)
        {
            var student = new Student
            {
                GradeId = request.GradeId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                IsActive = true
            };

            _context.Students.Add(student);
            _context.SaveChanges();
        }


        [HttpPost]
        public List<UserResponseV1> SearchStudents([FromBody] UserFilterRequestV1 filter)
        {
            var students = _context.Students
                .Include(s => s.Grade)
                .Where(s =>
                    s.IsActive &&
                    s.Grade.IsActive &&
                    (filter.FirstName == null || s.FirstName.Contains(filter.FirstName)) &&
                    (filter.LastName == null || s.LastName.Contains(filter.LastName)) &&
                    (filter.Email == null || s.Email.Contains(filter.Email)))
                .OrderByDescending(s => s.LastName)
                .ToList();

            var response = students.Select(s => new UserResponseV1
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Phone = s.Phone,
                Email = s.Email,
                GradeName = s.Grade.Name
            }).ToList();

            return response;
        }

        [HttpPost]
        public List<UserResponseV2> GetStudentsByGrade([FromBody] UserFilterRequestV2 filter)
        {
            var students = _context.Students
                .Include(s => s.Grade)
                .Where(s =>
                    s.IsActive &&
                    s.Grade.IsActive &&
                    (filter.FirstName == null || s.FirstName.Contains(filter.FirstName)) &&
                    (filter.GradeName == null || s.Grade.Name.Contains(filter.GradeName)))
                .Select(s => new
                {
                    Student = s,
                    FirstCourseName = s.Enrollments
                        .Where(e => e.IsActive && e.Course.IsActive)
                        .OrderByDescending(e => e.Course.Name)
                        .Select(e => e.Course.Name)
                        .FirstOrDefault()
                })
                .OrderByDescending(x => x.FirstCourseName)
                .ToList();

            var response = students.Select(x => new UserResponseV2
            {
                StudentId = x.Student.StudentId,
                FirstName = x.Student.FirstName,
                LastName = x.Student.LastName,
                Phone = x.Student.Phone,
                Email = x.Student.Email,
                Grade = new GradeResponseV1
                {
                    GradeId = x.Student.Grade.GradeId,
                    Name = x.Student.Grade.Name,
                    Description = x.Student.Grade.Description
                }
            }).ToList();

            return response;
        }

        [HttpGet("{id}")]
        public UserResponseV1 GetById(int id)
        {
            var student = _context.Students
                .Include(s => s.Grade)
                .FirstOrDefault(s => s.StudentId == id && s.IsActive && s.Grade.IsActive);

            if (student == null)
            {
                return null; 
            }

            var response = new UserResponseV1
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Phone = student.Phone,
                Email = student.Email,
                GradeName = student.Grade.Name
            };

            return response;
        }


        [HttpPut("{id}")]
        public void UpdateContactInfo(int id, [FromBody] UserRequestV2 request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id && s.IsActive);

            if (student == null)
            {
                return;
            }

            student.Phone = request.Phone;
            student.Email = request.Email;

            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void UpdatePersonalInfo(int id, [FromBody] UserRequestV3 request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id && s.IsActive);

            if (student == null)
            {
                return;
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;

            _context.SaveChanges();
        }

        [HttpGet("{id}")]
        public UserResponseV3 GetCoursesByStudent(int id)
        {
            var student = _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.StudentId == id && s.IsActive);

            if (student == null)
            {
                return null;
            }

            var courses = student.Enrollments
                .Where(e => e.IsActive && e.Course.IsActive)
                .Select(e => new CourseResponseV1
                {
                    CourseId = e.Course.CourseId,
                    Name = e.Course.Name,
                    Credit = e.Course.Credit
                }).ToList();

            return new UserResponseV3
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Phone = student.Phone,
                Email = student.Email,
                Courses = courses
            };
        }


    }
}
