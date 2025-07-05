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
    public class EnrollmentsCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public EnrollmentsCustomController(DemoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void Create([FromBody] EnrollmentRequestV1 request)
        {
            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                Date = request.Date,
                IsActive = true
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }

        [HttpPost]
        public List<EnrollmentResponseV1> GetEnrollmentsByCourse([FromBody] EnrollmentFilterRequestV1 filter)
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                    .ThenInclude(s => s.Grade)
                .Where(e =>
                    e.IsActive &&
                    e.Course.IsActive &&
                    e.Student.IsActive &&
                    e.Student.Grade.IsActive &&
                    (filter.CourseName == null || e.Course.Name.Contains(filter.CourseName)))
                .OrderBy(e => e.Course.Name)
                .ThenBy(e => e.Student.LastName)
                .ToList();

            var response = enrollments.Select(e => new EnrollmentResponseV1
            {
                EnrollmentId = e.EnrollmentId,
                Date = e.Date,
                Course = new CourseResponseV1
                {
                    CourseId = e.Course.CourseId,
                    Name = e.Course.Name,
                    Credit = e.Course.Credit
                },
                Student = new UserResponseV1
                {
                    StudentId = e.Student.StudentId,
                    FirstName = e.Student.FirstName,
                    LastName = e.Student.LastName,
                    Phone = e.Student.Phone,
                    Email = e.Student.Email,
                    GradeName = e.Student.Grade.Name
                }
            }).ToList();

            return response;
        }

        [HttpPost]
        public List<EnrollmentResponseV1> GetEnrollmentsByGrade([FromBody] EnrollmentFilterRequestV2 filter)
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                    .ThenInclude(s => s.Grade)
                .Where(e =>
                    e.IsActive &&
                    e.Course.IsActive &&
                    e.Student.IsActive &&
                    e.Student.Grade.IsActive &&
                    (filter.GradeName == null || e.Student.Grade.Name.Contains(filter.GradeName)))
                .OrderBy(e => e.Course.Name)
                .ThenBy(e => e.Student.LastName)
                .ToList();

            var response = enrollments.Select(e => new EnrollmentResponseV1
            {
                EnrollmentId = e.EnrollmentId,
                Date = e.Date,
                Course = new CourseResponseV1
                {
                    CourseId = e.Course.CourseId,
                    Name = e.Course.Name,
                    Credit = e.Course.Credit
                },
                Student = new UserResponseV1
                {
                    StudentId = e.Student.StudentId,
                    FirstName = e.Student.FirstName,
                    LastName = e.Student.LastName,
                    Phone = e.Student.Phone,
                    Email = e.Student.Email,
                    GradeName = e.Student.Grade.Name
                }
            }).ToList();

            return response;
        }

        [HttpPost]
        public void Enroll([FromBody] EnrollmentRequestV2 request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == request.StudentId && s.IsActive);
            if (student == null)
            {
                return;
            }

            var existingEnrollments = _context.Enrollments
                .Where(e => e.StudentId == request.StudentId)
                .ToList();

            var existingCourseIds = existingEnrollments.Select(e => e.CourseId).ToList();

            var newCourseIds = request.CourseIds
                .Where(cid => !existingCourseIds.Contains(cid))
                .ToList();

            foreach (var enrollment in existingEnrollments)
            {
                if (request.CourseIds.Contains(enrollment.CourseId) && !enrollment.IsActive)
                {
                    enrollment.IsActive = true;
                    enrollment.Date = DateTime.Now;
                }
            }

            var newEnrollments = newCourseIds.Select(cid => new Enrollment
            {
                StudentId = request.StudentId,
                CourseId = cid,
                Date = DateTime.Now,
                IsActive = true
            }).ToList();

            _context.Enrollments.AddRange(newEnrollments);
            _context.SaveChanges();
        }



    }
}
